using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.IO;
using Android.Provider;
using Android.Graphics;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;///must be aded from references     and sistem net  too
using Newtonsoft.Json;
using Core.Android.MicrosoftServices.MicrosoftModels;

namespace Core.Android.MicrosoftServices
{
   public  class MicrosoftVisualDescription
    {
        private string MyApiLocation { get; set; } ///ex-   westcentralus
        private string MyApiKey { get; set; }/// ex-   b945399a0a4b413aadb3bf63634f8635
        private string ChosenVisualFetures { get; set; }///ex- Description   or multi chose Description,Tags,Categories  
        private byte[] byteData;
        public string _imagePath;
        public AnalisisVisionResultModel ResultDescription;
        public Action<AnalisisVisionResultModel> OnReponse;


        public MicrosoftVisualDescription(string ApiLocation, string ApiKey, string chosenVisualFetures, string imageFilePath)
        {
            MyApiKey = ApiKey;
            MyApiLocation = ApiLocation;
            ChosenVisualFetures = chosenVisualFetures;
            byteData = System.IO.File.ReadAllBytes(imageFilePath); ;//// GetImageAsByteArray(imageFilePath);
            MakeRequest(MyApiLocation, MyApiKey, ChosenVisualFetures);
        }


        public async void MakeRequest(string myApiLocation, string myApiKey, string chossenVisualFeatures)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers   client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key/*this is api key*/", "{subscription key}");
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", myApiKey);
            // Request parameters
            queryString["visualFeatures"] = chossenVisualFeatures;///"Categories";
          //  queryString["details"] = "Celebrities";///"{string}";
            queryString["language"] = "en";
            var uri = "https://" + myApiLocation + ".api.cognitive.microsoft.com/vision/v1.0/analyze?" + queryString;//// this is muy location api----- westcentralus

            HttpResponseMessage response;

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/octet-stream" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                response = await client.PostAsync(uri, content);

                // Get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();
                ResultDescription = JsonConvert.DeserializeObject<AnalisisVisionResultModel>(contentString);
                OnReponse?.Invoke(ResultDescription);////is used for await for ending all threads before this.
            }

            byte[] GetImageAsByteArray(string imageFilePath)
            {
                System.IO.FileStream fileStream = new System.IO.FileStream(imageFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }



            //using (var content = new ByteArrayContent(byteData))
            //{
            //    content.Headers.ContentType = new MediaTypeHeaderValue("< your content type, i.e. application/json >");
            //    response = await client.PostAsync(uri, content);
            //}

        }


    }
}