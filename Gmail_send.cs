var fromAddress = new MailAddress("smadhavan1995@gmail.com", "Madhavan Seshadri");
                var toAddress = new MailAddress("noopijain@gmail.com", "Noopur Jain");
                const string fromPassword = "madu1810";
                //Email notification sending start 
                const string subject = "CMS Test";
                const string body = "Hello Noopur Jain! Dont be alarmed, if you are reading this, then it means that 'email_send' test function executed successfully";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
