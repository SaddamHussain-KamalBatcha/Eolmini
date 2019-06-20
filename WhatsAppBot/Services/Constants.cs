using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatsAppBot.Services
{
    public static class Constants
    {
        public static readonly string WelcomeMessage = $"Dear EOL User,Welcome to EOL Mini.\n How can we help you? \n Here are the quick options: \n 1. Re order your previous order \n 2. Get your order status \n 3. Know your nearest drop off location \n 4.Learn What's new in EOL \n 5. Suggest new feauture on EOL Mini \n 6.Provide your feedback";
    
        public static readonly string ApiExceptionMessage = @"EOl server is down, Could not able to reorder.Please try again later.";

        public static readonly string ReOrderSuceessMessage = @"Order has been succesfully placed.";

        public static readonly string ReOrderFailureMessage = @"This order cannot be reordered through whatsapp.We have encountered some problem with this order.This is because of either Quotation upgrade or tests upgrade.Please complete the order in the website from pending order menu";

        public static readonly string ValidatedOrdersMessage = @"Please send the option number to reorder.Here are the quick options:";

        public static readonly string NoOrderMessage = @"You don't have any order to reorder.Please create Order through EOL mobile app or EOL website to reorder";

        public static readonly string AllOrdersMessage = @"Please send the option number of order to get order status .Here are the quick options:";
							                                       

        public static readonly string OrderStatusMessage = @"Order Xoxo has been imported by lab //validated //locked ";

        public static readonly string ShareYourLocationMessage = @"Please share your current location";

        public static readonly string DropOffLocationMessage = @"DropOffLocationAddress";

        public static readonly string SuggestNewFeaturesMessage = @" Please type in the feature you want to see:";

        public static readonly string ThanksForNewFeautresMessage = @" Thank you for your interest in this feature, we will try to accomodate it in future upgrades";

        public static readonly string ReleaseMessage = @" Following are new features in upcoming EOL release:";

        public static readonly string FeedbackMessage = @" Please provide your feedback:";

        public static readonly string ThanksForFeedbackMessage = @"Thank you for your feedback.";

    }
}