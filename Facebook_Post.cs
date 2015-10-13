string app_id = "155653304783870";
            string app_secret = "20f17d664ab5cb946334ac33a8db3ea6";
            string scope = "publish_pages,manage_pages";

            if (Request["code"] == null)
            {
                Response.Redirect(string.Format(
                    "https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}",
                    app_id, Request.Url.AbsoluteUri, scope));
            }
            else
            {
                Dictionary<string, string> tokens = new Dictionary<string, string>();

                string url = string.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&scope={2}&code={3}&client_secret={4}",
                    app_id, Request.Url.AbsoluteUri, scope, Request["code"].ToString(), app_secret);

                HttpWebRequest request = System.Net.WebRequest.Create(url) as HttpWebRequest;

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    string vals = reader.ReadToEnd();

                    foreach (string token in vals.Split('&'))
                    {
                        //meh.aspx?token1=steve&token2=jake&...
                        tokens.Add(token.Substring(0, token.IndexOf("=")),
                            token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
                    }
                }

                string access_token = tokens["access_token"];
                var client = new FacebookClient(access_token);

                dynamic me = client.Get("me/accounts/428266620709236?fields=access_token");
                
                string page_token = me["data"][1]["access_token"].ToString();
                var client1 = new FacebookClient(page_token);
                //ViewBag.Message = page_token;

                //return View();
                //me / accounts ? fields = access_token
                //446533181408238 is my fan page
                //client.Post("/428266620709236/feed/", parameters);
                string mdd = "CMS Test:~ New Hello World from CMS Thunder Groudon posted on behalf of Madhavan Seshadri";
                dynamic result1 = client1.Post("428266620709236/feed",new { message = mdd}
                );
