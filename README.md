# BotFrameworkIntgrationPowerBIEmbedded
Publish Project on Github
This is a reference project for how to intgration an Bot Framework project with Power BI Embedded in this Project Bot used Card and Action model give more interactive！
And used Bot Framework link to power BI Embedded page make the user search BI chart more simple and more accurately.
This PowerBI Embedded project is adjusted for China Azure Mooncake version. for more infomation please reference my bolg: http://www.cnblogs.com/sonic1abc/p/6602530.html
                

                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                Activity replyToConversation = activity.CreateReply("Hi, report is here！");
                replyToConversation.Recipient = activity.From;
                replyToConversation.Type = "message";
                replyToConversation.Attachments = new List<Attachment>();

                List<CardImage> cardImages = new List<CardImage>();
                //这里是三方SDK直接URL传参返回截图
                cardImages.Add(new CardImage(url: "http://api.page2images.com/directlink?p2i_url=http://pbie.chinacloudsites.cn/Dashboard/Report?reportId=0a46b754-f5a3-4c19-b032-c6599bdb2f4d&p2i_key=d63f8178f119c069"));

                List<CardAction> cardButtons = new List<CardAction>();

                CardAction plButton = new CardAction()
                {
                    Value = "http://pbie.chinacloudsites.cn/Dashboard/Report?reportId=0a46b754-f5a3-4c19-b032-c6599bdb2f4d",
                    Type = "openUrl",
                    Title = "Show me details!"
                };

                cardButtons.Add(plButton);

                HeroCard plCard = new HeroCard()
                {
                    Title = "Report",
                    //Subtitle = "详细页",
                    Images = cardImages,
                    Buttons = cardButtons
                };

                Attachment plAttachment = plCard.ToAttachment();
                replyToConversation.Attachments.Add(plAttachment);

                var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);

                //context.Wait(MessageReceivedAsync);
                #endregion
