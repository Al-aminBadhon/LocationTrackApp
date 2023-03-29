namespace GetLocationApp
{
    public class GeoLocationHelper
    {
        private readonly HttpClient _httpClient;
        public GeoLocationHelper()
        {
            _httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(5)
            };

        }

        public async Task<string> GetIPAddress()
        {
            //getting current pc ip address
            var ipAddress = await _httpClient.GetAsync($"http://ipinfo.io/ip");
            if(ipAddress.IsSuccessStatusCode)
            {
                var json = ipAddress.Content.ReadAsStringAsync();
                return json.Result.ToString();
            }
            return "";
        }

        public async Task<string> GetGeoInfo()
        {
            var ipAddress = await GetIPAddress(); //calling for IpAddress

            var response = await _httpClient.GetAsync($"http://api.ipstack.com/" + ipAddress +
                "?access_key=501e22926789498dae1431cbd82cbd6e"); //we need to create a free account to get access key
            if(response.IsSuccessStatusCode)
            {
                var jsonInfo = response.Content.ReadAsStringAsync();
                return jsonInfo.Result.ToString();
            }

            return "";
        }

    }
}
