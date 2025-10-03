using System.Text.Json;
using System.Text;
using HtmlAgilityPack;
using static FutsoLig.Models;
using System.Net;

namespace FutsoLig
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // HttpClient'ın GZip desteği ile başlatılması
            client = InitializeHttpClient();

            Load();
            StartAutoUpdateLoop();
        }

        // HttpClient'ı artık sadece burada readonly olarak değil, fonksiyonla başlatacağız.
        private static HttpClient client;

        // API URL'leri
        private static string EventsApiUrl = "https://ticketingweb.passo.com.tr/api/passoweb/allevents";
        private static string GetCatUrl(string eventId) =>
            $"https://cppasso2.mediatriple.net/30s/api/passoweb/getcategories?eventId={eventId}&serieId=&tickettype=100&campaignId=null&validationintegrationid=null";
        private static string GetBlocksUrl(string eventId, string catId) =>
            $"https://cppasso2.mediatriple.net/30s/api/passoweb/getavailableblocklist?eventId={eventId}&serieId=&seatCategoryId={catId}";

        // Statik değişkenler
        static string _bearerToken = "";
        static string eventId = "";
        static string catId = "";
        static bool isLoop = true;

        private CancellationTokenSource? cts;
        private CancellationTokenSource? autoUpdateCts;

        private List<EventItem>? currentEvents;
        private List<CategoryItem>? currentCategories;

        private string VERSION = "1.1";

        // *** 1. HATA DÜZELTMESİ: HttpClient'ı GZip desteği ile başlatan metot ***
        private static HttpClient InitializeHttpClient()
        {
            var handler = new HttpClientHandler
            {
                // GZip ve Deflate sıkıştırmasını otomatik açmayı etkinleştirir
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            var client = new HttpClient(handler);

            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/139.0.0.0 Safari/537.36 Edg/139.0.0.0");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Referer", "https://www.passo.com.tr/");
            client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

            return client;
        }


        // *** 2. HATA DÜZELTMESİ: Başlık Hazırlama (Clear() kaldırıldı, sadece Authorization yönetiliyor) ***
        private static void PrepareCommonHeaders(bool includeAuth, string? bearerToken = null)
        {
            // Önceki Authorization başlığını kaldırın (sadece değişen başlık için bu yapılır)
            client.DefaultRequestHeaders.Remove("Authorization");

            if (includeAuth && !string.IsNullOrWhiteSpace(bearerToken))
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");
            }
        }


        public async void Load()
        {
            cts = new CancellationTokenSource();
            var events = await GetEventsAsync(cts.Token);
            currentEvents = events;

            listBoxEvents.Items.Clear();
            richTextBox1.Clear();

            if (events.Count == 0)
            {
                richTextBox1.Text = "Hiç etkinlik bulunamadı.";
                return;
            }

            for (int i = 0; i < events.Count; i++)
            {
                listBoxEvents.Items.Add($"{i + 1}) {events[i].Name} - {events[i].Date:yyyy-MM-dd HH:mm}");
            }

            richTextBox1.Text = "Etkinlik listesi yüklendi. Lütfen bir seçim yapın.";
        }

        private async void listBoxEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxEvents.SelectedIndex >= 0 && currentEvents != null)
            {
                eventId = currentEvents[listBoxEvents.SelectedIndex].id.ToString();
                textBoxEventId.Text = eventId;

                cts = new CancellationTokenSource();

                var categories = await GetCategoriesAsync(cts.Token);

                currentCategories = categories;

                listBoxCategories.Items.Clear();
                richTextBox1.Clear();

                if (categories.Count == 0)
                {
                    richTextBox1.Text = "Bu etkinlik için kategori bulunamadı veya erişim engeli var.";
                    return;
                }

                // Listeyi doldur
                for (int i = 0; i < categories.Count; i++)
                {
                    var c = categories[i];
                    string info = c.Message != null ? $"({c.Message})" : "";
                    listBoxCategories.Items.Add($"{i + 1}) {c.Name} - {c.FormattedPrice} {info}");
                }

                richTextBox1.Text = "Kategori listesi yüklendi. Lütfen bir seçim yapın.";
            }
        }

        private async void listBoxCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxCategories.SelectedIndex >= 0 && currentCategories != null)
            {
                catId = currentCategories[listBoxCategories.SelectedIndex].Id.ToString();
                textBoxCatId.Text = catId;
                cts = new CancellationTokenSource();

                var blocks = await GetBlocksAsync(cts.Token);

                listBoxBlocks.Items.Clear();
                richTextBox1.Clear();

                if (blocks.Count == 0)
                {
                    richTextBox1.Text = ("Bu kategori için blok bulunamadı veya erişim engeli var.");
                    return;
                }

                // Listeyi doldur
                for (int i = 0; i < blocks.Count; i++)
                {
                    var b = blocks[i];
                    string count = b.TotalCount != null ? $"({b.TotalCount})" : "";
                    listBoxBlocks.Items.Add($"{i + 1}) {b.Name} {count}");
                }
            }
        }

        private static async Task<List<EventItem>> GetEventsAsync(CancellationToken cancellationToken)
        {
            PrepareCommonHeaders(includeAuth: false);

            var payload = new EventsRequest
            {
                SubGenreId = "8617",
                LanguageId = 118,
                from = 0,
                size = 53
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            try
            {
                var json = JsonSerializer.Serialize(payload, jsonOptions);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(EventsApiUrl, content, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    return new List<EventItem>();
                }

                var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
                var result = JsonSerializer.Deserialize<PassoResponse>(responseBody);
                return result?.ValueList ?? new List<EventItem>();
            }
            catch
            {
                return new List<EventItem>();
            }
        }

        private static async Task<List<CategoryItem>> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            PrepareCommonHeaders(includeAuth: true, _bearerToken);

            try
            {
                var url = GetCatUrl(eventId);
                var response = await client.GetAsync(url, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    return new List<CategoryItem>();
                }

                var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

                if (string.IsNullOrWhiteSpace(responseBody))
                    return new List<CategoryItem>();

                var result = JsonSerializer.Deserialize<CategoryResponse>(responseBody);
                return result?.ValueList ?? new List<CategoryItem>();
            }
            catch
            {
                return new List<CategoryItem>();
            }
        }

        private static async Task<List<BlockItem>> GetBlocksAsync(CancellationToken cancellationToken)
        {
            PrepareCommonHeaders(includeAuth: true, _bearerToken);

            try
            {
                var url = GetBlocksUrl(eventId, catId);
                var response = await client.GetAsync(url, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    return new List<BlockItem>();
                }

                var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

                if (string.IsNullOrWhiteSpace(responseBody))
                    return new List<BlockItem>();

                var result = JsonSerializer.Deserialize<BlockResponse>(responseBody);
                return result?.ValueList ?? new List<BlockItem>();
            }
            catch (Exception ex)
            {
                return new List<BlockItem>();
            }
        }
        private void StartAutoUpdateLoop()
        {
            autoUpdateCts?.Cancel();
            autoUpdateCts = new CancellationTokenSource();
            var token = autoUpdateCts.Token;

            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested && isLoop)
                {
                    try
                    {
                        // Sadece Event seçilmişse kategori güncelle (Event listesi sadece Load'da güncellenmeli)
                        if (!string.IsNullOrWhiteSpace(eventId))
                        {
                            var categories = await GetCategoriesAsync(token);
                            currentCategories = categories;

                            // UI güncellemesi
                            listBoxCategories.Invoke((Action)(() =>
                            {
                                listBoxCategories.Items.Clear();
                                if (categories.Count > 0)
                                {
                                    for (int i = 0; i < categories.Count; i++)
                                    {
                                        var c = categories[i];
                                        string info = c.Message != null ? $"({c.Message})" : "";
                                        listBoxCategories.Items.Add($"{i + 1}) {c.Name} - {c.FormattedPrice} {info}");
                                    }
                                }
                            }));
                        }

                        // Event ve kategori seçilmişse blokları güncelle
                        if (!string.IsNullOrWhiteSpace(eventId) && !string.IsNullOrWhiteSpace(catId))
                        {
                            var blocks = await GetBlocksAsync(token);

                            // UI güncellemesi
                            listBoxBlocks.Invoke((Action)(() =>
                            {
                                listBoxBlocks.Items.Clear();
                                if (blocks.Count > 0)
                                {
                                    for (int i = 0; i < blocks.Count; i++)
                                    {
                                        var b = blocks[i];
                                        listBoxBlocks.Items.Add($"{i + 1}) {b.Name} ({b.TotalCount})");
                                    }
                                }
                            }));
                        }

                        // RichTextBox güncellemesi sadece bir kere yapılabilir
                        if (richTextBox1.InvokeRequired)
                        {
                            richTextBox1.Invoke((Action)(() =>
                            {
                                richTextBox1.Text += $"\nGüncelleme tamamlandı. {DateTime.Now}";
                            }));
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch
                    {
                    
                    }

                    await Task.Delay(5000, token);
                }
            }, token);
        }

        private void StopAutoUpdateLoop()
        {
            isLoop = false;
            autoUpdateCts?.Cancel();
        }

        // --- Diğer Olay İşleyicileri (TextChanged, Click vb.) değişmedi ---

        private void textBoxBearer_TextChanged(object sender, EventArgs e)
        {
            _bearerToken = textBoxBearer.Text.Trim();
        }

        private void Loop_Click(object sender, EventArgs e)
        {
            if (!isLoop)
            {
                isLoop = true;
                LoopButton.Text = "Döngüyü Durdur";
                StartAutoUpdateLoop();
            }
            else
            {
                isLoop = false;
                LoopButton.Text = "Döngüyü Başlat";
                StopAutoUpdateLoop();
            }
        }

        private void toolStripUpdate_Click(object sender, EventArgs e)
        {
            CheckUpdate();
        }

        private void textBoxEventId_TextChanged(object sender, EventArgs e)
        {
            eventId = textBoxEventId.Text.Trim();
        }

        private void toolStripContact_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://chareless.github.io/saribayirdeniz/#contact",
                    UseShellExecute = true
                };
                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Site Açılamadı: " + ex.Message);
            }
        }

        private void CheckUpdate()
        {
            try
            {
                Uri url = new Uri("https://chareless.github.io/saribayirdeniz/futsolig.html");
                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    string html = client.DownloadString(url);
                    HtmlAgilityPack.HtmlDocument dokuman = new HtmlAgilityPack.HtmlDocument();
                    dokuman.LoadHtml(html);
                    HtmlNodeCollection titles = dokuman.DocumentNode.SelectNodes("/html/body/div/div/div[2]/div/div/div/div/div[2]/div[2]/div/div/p[5]/h7");
                    string version = "";

                    if (titles != null)
                    {
                        foreach (HtmlNode title in titles)
                        {
                            version = title.InnerText;
                        }
                    }

                    if (version == VERSION)
                    {
                        MessageBox.Show("Sürüm Güncel", "Güncellemeleri Denetle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Güncelleme Mevcut. Yeni Sürümü İndirmek İster Misiniz?", "Güncellemeleri Denetle",
                             MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                        if (result == DialogResult.OK)
                        {
                            try
                            {
                                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo
                                {
                                    FileName = "https://github.com/chareless/FutsoLig-for-Windows/archive/master.zip",
                                    UseShellExecute = true
                                };
                                System.Diagnostics.Process.Start(psi);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("İndirme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Güncel Sürüm Bulunurken Hata!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}