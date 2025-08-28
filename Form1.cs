using System.Text.Json;
using System.Text;
using static FutsoLig.Models;

namespace FutsoLig
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Load();
            StartAutoUpdateLoop();
        }

        private static readonly HttpClient client = new HttpClient();

        private static string EventsApiUrl = "https://ticketingweb.passo.com.tr/api/passoweb/allevents";

        private static string GetCatUrl(string eventId) =>
            $"https://cppasso2.mediatriple.net/30s/api/passoweb/getcategories?eventId={eventId}&serieId=&tickettype=100&campaignId=null&validationintegrationid=null";

        private static string GetBlocksUrl(string eventId, string catId) =>
            $"https://cppasso2.mediatriple.net/30s/api/passoweb/getavailableblocklist?eventId={eventId}&serieId=&seatCategoryId={catId}";


        static string _bearerToken = "";
        static string eventId = "";
        static string catId = "";

        static bool isLoop = true;

        private CancellationTokenSource? cts;
        private CancellationTokenSource? autoUpdateCts;

        private List<EventItem>? currentEvents;
        private List<CategoryItem>? currentCategories;

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
            // Önce eski loop varsa iptal et
            autoUpdateCts?.Cancel();
            autoUpdateCts = new CancellationTokenSource();
            var token = autoUpdateCts.Token;

            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        // Event seçilmişse kategori güncelle
                        var events = await GetEventsAsync(token);
                        listBoxEvents.Invoke((Action)(() =>
                        {
                            listBoxCategories.Items.Clear();
                            if (events.Count > 0)
                            {
                                for (int i = 0; i < events.Count; i++)
                                {
                                    listBoxEvents.Items.Add($"{i + 1}) {events[i].Name} - {events[i].Date:yyyy-MM-dd HH:mm}");
                                }
                            }
                        }));

                        if (!string.IsNullOrWhiteSpace(eventId))
                        {
                            var categories = await GetCategoriesAsync(token);
                            currentCategories = categories;

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
                            if (richTextBox1.InvokeRequired)
                            {
                                richTextBox1.Invoke((Action)(() =>
                                {
                                    richTextBox1.Text += $"\nKategori listesi güncellendi. {DateTime.Now}";
                                }));
                            }
                            else
                            {
                                richTextBox1.Text += $"\nKategori listesi güncellendi. {DateTime.Now}";
                            }
                        }

                        // Event ve kategori seçilmişse blokları güncelle
                        if (!string.IsNullOrWhiteSpace(eventId) && !string.IsNullOrWhiteSpace(catId))
                        {
                            var blocks = await GetBlocksAsync(token);

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

                        if (richTextBox1.InvokeRequired)
                        {
                            richTextBox1.Invoke((Action)(() =>
                            {
                                richTextBox1.Text += $"\nBlok listesi güncellendi. {DateTime.Now}";
                            }));
                        }
                        else
                        {
                            richTextBox1.Text += $"\nBlok listesi güncellendi. {DateTime.Now}";
                        }
                    }
                    catch
                    {
                        // Hata olursa loop devam etsin
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

        private static void PrepareCommonHeaders(bool includeAuth, string? bearerToken = null)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/139.0.0.0 Safari/537.36 Edg/139.0.0.0");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Referer", "https://www.passo.com.tr/");
            client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

            if (includeAuth && !string.IsNullOrWhiteSpace(bearerToken))
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");
            }
        }

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

        }

        private void textBoxEventId_TextChanged(object sender, EventArgs e)
        {
            eventId = textBoxEventId.Text.Trim();
        }
    }
}