using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Eventmaker.Model;
using Newtonsoft.Json;

namespace NoteMVVM
{
    //Json.Net er downloaded til projektet via NuGet: Højreklik på projektet -> Manage NuGet Package

    class PersistencyService
    {
        
        public static async void SaveEventsAsJsonAsync(Event events)
        { 
            const string serverUrl = "http://localhost:55337";
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (HttpClient client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();

                try
                {
                    await client.PostAsJsonAsync("api/events", events);
                }
                catch (Exception e)
                {
                    new MessageDialog(e.Message).ShowAsync();
                }
            }
        }

        public static async void DeleteEventsAsync(Event events)
        {
            const string serverUrl = "http://localhost:55337";
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (HttpClient client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();

                try
                {
                    await client.DeleteAsync("api/events/" + events.Id);
                }
                catch (Exception e)
                {
                    new MessageDialog(e.Message).ShowAsync();
                    throw;
                }
            }
        }

        public static async Task<List<Event>> LoadEventsFromJsonAsync()
        {
            const string serverUrl = "http://localhost:55337";
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (HttpClient client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();

                try
                {
                    var response = client.GetAsync("api/events").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var eventdata = response.Content.ReadAsAsync<IEnumerable<Event>>().Result;
                        return eventdata.ToList();
                    }

                    return null;
                }
                catch (Exception e)
                {
                    new MessageDialog(e.Message).ShowAsync();
                    throw;
                }
            }
        }

        public static async void UpdateEventAsync(Event ev)
        {
            const string serverUrl = "http://localhost:55337";
            HttpClientHandler handler = new HttpClientHandler(); 
            handler.UseDefaultCredentials = true;

            using (HttpClient client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear(); // <- Not sure is needed...

                try
                {
                    
                    //string postBody = JsonConvert.SerializeObject(ev);
                    //var response = await client.PutAsync("api/events/" + ev.Id, new StringContent(postBody, Encoding.UTF8, "application/json"));
                    var response = await client.PutAsJsonAsync("api/events/" + ev.Id, ev);

                }
                catch (Exception e)
                {
                    new MessageDialog(e.Message).ShowAsync();
                    throw;
                }
            }
        }

        //    private static string JsonFileName = "Events.json";

        //    public static async void SaveEventsAsJsonAsync(ObservableCollection<Event> notes)
        //    {
        //        string notesJsonString = JsonConvert.SerializeObject(notes);
        //        SerializeNotesFileAsync(notesJsonString, JsonFileName);
        //    }

        //    public static async Task<List<Event>> LoadEventsFromJsonAsync()
        //    {
        //        string notesJsonString = await DeserializeNotesFileAsync(JsonFileName);
        //        if (notesJsonString != null)
        //            return (List<Event>) JsonConvert.DeserializeObject(notesJsonString, typeof (List<Event>));
        //        return null;
        //    }



        //    private static async void SerializeNotesFileAsync(string notesJsonString, string fileName)
        //    {
        //        StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
        //        await FileIO.WriteTextAsync(localFile, notesJsonString);
        //    }


        //    private static async Task<string> DeserializeNotesFileAsync(string fileName)
        //    {
        //        try
        //        {
        //            StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
        //            return await FileIO.ReadTextAsync(localFile);
        //        }
        //        catch (FileNotFoundException ex)
        //        {
        //          //  MessageDialogHelper.Show("Loading for the first time? - Try Add and Save some Notes before trying to Save for the first time", "File not Found");
        //            return null;
        //        }
        //    }


        //    private class MessageDialogHelper
        //    {
        //        public static async void Show(string content, string title)
        //        {
        //            MessageDialog messageDialog = new MessageDialog(content, title);
        //            await messageDialog.ShowAsync();
        //        }
        //    }

    }
}