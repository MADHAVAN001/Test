            //Please change here
            string app_id = "application_id";
            string app_secret = "application_secret";
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
                        tokens.Add(token.Substring(0, token.IndexOf("=")),
                            token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
                    }
                }

                string access_token = tokens["access_token"];
                var client = new FacebookClient(access_token);
                
                //Please change here
                dynamic me = client.Get("me/accounts/pageId?fields=access_token");
                
                string page_token = me["data"][1]["access_token"].ToString();
                var client1 = new FacebookClient(page_token);
                
                string mdd = "CMS Test:~ New Hello World from CMS Thunder Groudon posted on behalf of Madhavan Seshadri";\
                            
                //Please change here
                dynamic result1 = client1.Post("pageId/feed",new { message = mdd}
                );
