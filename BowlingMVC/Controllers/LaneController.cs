using BowlingMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BowlingMVC.Controllers
{
    public class LaneController : Controller
    {   
        //Hosted web API REST Service base url  
        string baseUrl = "https://localhost:7197/api/Lanes/";
        public async Task<ActionResult> Index()
        {
            List<Lane> laneInfo = new List<Lane>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync(baseUrl);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var laneResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    laneInfo = JsonConvert.DeserializeObject<List<Lane>>(laneResponse);

                }
                //returning the lane list to view  
                return View(laneInfo);
            }


        }
    }
}