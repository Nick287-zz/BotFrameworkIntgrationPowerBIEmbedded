using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace Bot_Application.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            if (activity.Text.ToLower().Contains("hi") || activity.Text.ToLower().Contains("hello"))
            {
                await context.PostAsync("Hi, I can show the report for you, do you want it?");
            }
            else if (activity.Text.ToLower().Contains("yes") || activity.Text.ToLower().Contains("ok") || activity.Text.ToLower().Contains("go"))
            {
                #region HeroCard

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
            }
            else if (!string.IsNullOrWhiteSpace(activity.Text))
            {
                //calculate something for us to return
                int length = (activity.Text ?? string.Empty).Length;
                // return our reply to the user
                await context.PostAsync("sorry, I'm trying to learn more, so far I can show the report for you, do you want it?");
                //context.Wait(MessageReceivedAsync);
            }

            context.Wait(MessageReceivedAsync);
        }
    }
}


#region sign-in card
//Activity replyToConversation = activity.CreateReply("Should go to conversation, sign-in card");
//replyToConversation.Recipient = activity.From;
//replyToConversation.Type = "message";
//replyToConversation.Attachments = new List<Attachment>();

//List<CardAction> cardButtons = new List<CardAction>();

//CardAction plButton = new CardAction()
//{
//    Value = "https://<OAuthSignInURL>",
//    Type = "signin",
//    Title = "Connect"
//};
//cardButtons.Add(plButton);

//SigninCard plCard = new SigninCard(text: "You need to authorize me", buttons: cardButtons);

//Attachment plAttachment = plCard.ToAttachment();
//replyToConversation.Attachments.Add(plAttachment);

//var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);
#endregion

#region thumbnail card

//Activity replyToConversation = activity.CreateReply("Should go to conversation, with a thumbnail card");
//replyToConversation.Recipient = activity.From;
//replyToConversation.Type = "message";
//replyToConversation.Attachments = new List<Attachment>();

//List<CardImage> cardImages = new List<CardImage>();
//cardImages.Add(new CardImage(url: "https://ss0.bdstatic.com/70cFvHSh_Q1YnxGkpoWK1HF6hhy/it/u=1761358280,3939647228&fm=11&gp=0.jpg"));
//cardImages.Add(new CardImage(url: "https://ss0.bdstatic.com/70cFvHSh_Q1YnxGkpoWK1HF6hhy/it/u=1242809026,774215096&fm=11&gp=0.jpg"));


//List<CardAction> cardButtons = new List<CardAction>();

//CardAction plButton = new CardAction()
//{
//    Value = "https://en.wikipedia.org/wiki/Pig_Latin",
//    Type = "openUrl",
//    Title = "WikiPedia Page"
//};
//cardButtons.Add(plButton);

//ThumbnailCard plCard = new ThumbnailCard()
//{
//    Title = "I'm a thumbnail card",
//    Subtitle = "Pig Latin Wikipedia Page",
//    Images = cardImages,
//    Buttons = cardButtons
//};

//Attachment plAttachment = plCard.ToAttachment();
//replyToConversation.Attachments.Add(plAttachment);
//replyToConversation.Attachments.Add(plAttachment);

//var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);
#endregion