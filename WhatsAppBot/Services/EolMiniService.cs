using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using WhatsAppBot.Database;
using WhatsAppBot.Models;

namespace WhatsAppBot.Services
{
    public class EolMiniService
    {
        private readonly EolMiniDataContext _dbContext;

        public string ReOrderMenu = "1";
        private string OrderStatusMenu = "2";
        private string DropOffLocationMenu = "3";
        private string NewInEOLMenu = "4";
        private string SuggestMenu = "5";
        private string FeedbackMenu = "6";

        public EolMiniService()
        {
            _dbContext = new EolMiniDataContext();
        }

        public IncomingRequest GetIncomingRequest(IncomingRequest request)
        {
            var userlastRecordsDb = _dbContext.ChatRecords.Where(m => m.MobileNumber == request.to).ToList();
            var userlastRecordDb = userlastRecordsDb.LastOrDefault();

            var incomingMessage = request.body;

            if (userlastRecordDb == null)
            {
                request.body = Constants.WelcomeMessage;
                SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.WelcomeMessage);
                return request;
            }

            if (userlastRecordDb.LastSentFormat == Constants.WelcomeMessage)
            {
                if (request.body == ReOrderMenu)
                {
                    GetValidatedOrder(request, userlastRecordDb, incomingMessage);
                }

                else if (request.body == OrderStatusMenu)
                {
                    GetAllOrders(request, userlastRecordDb, incomingMessage);
                }

                else if (request.body == DropOffLocationMenu)
                {
                    request.body = Constants.ShareYourLocationMessage;
                    SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.ShareYourLocationMessage);
                    return request;
                }

                else if (request.body == NewInEOLMenu)
                {
                    request.body = Constants.ReleaseMessage;
                    SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.ReleaseMessage);
                    return request;
                }

                else if (request.body == SuggestMenu)
                {
                    request.body = Constants.SuggestNewFeaturesMessage;
                    SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.SuggestNewFeaturesMessage);
                    return request;
                }

                else if (request.body == FeedbackMenu)
                {
                    request.body = Constants.FeedbackMessage;
                    SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.FeedbackMessage);
                    return request;
                }

                else
                {
                    request.body = Constants.WelcomeMessage;
                    SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.WelcomeMessage);
                    return request;
                }
            }
            else if (userlastRecordDb.LastSentFormat == Constants.ValidatedOrdersMessage)
            {
                if (request.body == "1")
                {
                    ReOrder(request, "1", userlastRecordDb, incomingMessage);
                    return request;
                }

                if (request.body == "2")
                {
                    ReOrder(request, "2", userlastRecordDb, incomingMessage);
                    return request;
                }

                if (request.body == "3")
                {
                    ReOrder(request, "3", userlastRecordDb, incomingMessage);
                    return request;
                }

                if (request.body == "4")
                {
                    ReOrder(request, "4", userlastRecordDb, incomingMessage);
                    return request;
                }

                if (request.body == "5")
                {
                    ReOrder(request, "5", userlastRecordDb, incomingMessage);
                    return request;
                }
                else
                {
                    request.body = Constants.WelcomeMessage;
                    SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.WelcomeMessage);
                    return request;
                }
            }
            else if (userlastRecordDb.LastSentFormat == Constants.AllOrdersMessage)
            {
                if (request.body == "1")
                {
                    GetOrderStatus(request, "1", userlastRecordDb, incomingMessage);
                    return request;

                }

                if (request.body == "2")
                {
                    GetOrderStatus(request, "2", userlastRecordDb, incomingMessage);
                    return request;

                }

                if (request.body == "3")
                {
                    GetOrderStatus(request, "3", userlastRecordDb, incomingMessage);
                    return request;

                }

                if (request.body == "4")
                {
                    GetOrderStatus(request, "4", userlastRecordDb, incomingMessage);
                    return request;

                }

                if (request.body == "5")
                {
                    GetOrderStatus(request, "5", userlastRecordDb, incomingMessage);
                    return request;

                }
                else
                {
                    request.body = Constants.WelcomeMessage;
                    SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.WelcomeMessage);
                    return request;
                }
            }
            else if(userlastRecordDb.LastSentFormat == Constants.ShareYourLocationMessage)
            {
                GetDropOffLocaction(request);
                SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.DropOffLocationMessage);
                return request;
            }
            else if (userlastRecordDb.LastSentFormat == Constants.SuggestNewFeaturesMessage)
            {
                SuggestNewFeatures(request);
                SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.ThanksForNewFeautresMessage);
                return request;
            }
            else if (userlastRecordDb.LastSentFormat == Constants.FeedbackMessage)
            {
                Feedback(request);
                SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.ThanksForFeedbackMessage);
                return request;
            }

            else
            {
                request.body = Constants.WelcomeMessage;
                SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.WelcomeMessage);
                return request;
            }

            return request;
        }

        private T WebGETSync<T>(string url)
        {
            try
            {
                //var baseUrl = ConfigurationManager.AppSettings["baseUrl"] + url;
                var baseUrl = "http://localhost:59131/api/ordering/Mobile" + url;
                var webHeader = new WebHeaderCollection()
                                {
                                     {HttpRequestHeader.Authorization, "Bearer " + "BE739D1B-C4FB-4CF6-A5F1-F07C81" },
                                     {"userName", "demouser" }
                                };

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl);
                request.Headers = webHeader;
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var sr = new StreamReader(response.GetResponseStream());
                var json = sr.ReadToEnd();

                var jsondata = JsonConvert.DeserializeObject<T>(json);
                return jsondata;
            }
            catch
            {
                throw new Exception("Service are down. Please try again later.");
            }
        }

        private void SaveSentResponse(IncomingRequest request, ChatRecord userlastRecordDb, string incomingMessage, string lastSentFormat)
        {
            if (userlastRecordDb == null)
            {
                var newRecord = new ChatRecord()
                {
                    LastReceivedMessage = incomingMessage,
                    SentOn = DateTime.Now,
                    LastSentFormat = lastSentFormat,
                    LastSentMessage = request.body,
                    MobileNumber = request.to
                };
                _dbContext.ChatRecords.Add(newRecord);
            }
            else
            {
                userlastRecordDb.LastSentMessage = request.body;
                userlastRecordDb.MobileNumber = request.to;
                userlastRecordDb.SentOn = DateTime.Now;
                userlastRecordDb.LastReceivedMessage = incomingMessage;
                userlastRecordDb.LastSentFormat = lastSentFormat;
            }
            _dbContext.SaveChanges();
        }

        private IncomingRequest ReOrder(IncomingRequest request, string orderOptionNumber, ChatRecord userlastRecordDb, string incomingMessage)
        {
            try
            {
                var url = "/getValidatedOrder";
                // var validatedOrders = WebGETSync<List<OrderResponse>>(url);

                var validatedOrders = new List<OrderResponse>()
                           {
                            new OrderResponse{Name="Order1",Id=111111,ValidatedOn=DateTime.Now},
                            new OrderResponse{Name="Order2",Id=222222,ValidatedOn=DateTime.Now},
                            new OrderResponse{Name="Order3",Id=333333,ValidatedOn=DateTime.Now},
                            new OrderResponse{Name="Order4",Id=444444,ValidatedOn=DateTime.Now},
                            new OrderResponse{Name="Order5",Id=555555,ValidatedOn=DateTime.Now},
                           };

                string reOrderUrl = "";

                if (orderOptionNumber == "1")
                {
                    reOrderUrl = $"/reorder?orderId={validatedOrders[0].Id}";
                }
                if (orderOptionNumber == "2")
                {
                    reOrderUrl = $"/reorder?orderId={validatedOrders[1].Id}";
                }
                if (orderOptionNumber == "3")
                {
                    reOrderUrl = $"/reorder?orderId={validatedOrders[2].Id}";
                }
                if (orderOptionNumber == "4")
                {
                    reOrderUrl = $"/reorder?orderId={validatedOrders[3].Id}";
                }
                if (orderOptionNumber == "5")
                {
                    reOrderUrl = $"/reorder?orderId={validatedOrders[4].Id}";
                }

                // var response = WebGETSync<ReorderResponse>(reOrderUrl);
                var response = new ReorderResponse { Step = "Review", OrderId = "123" };

                if (response.Step == OrderingStep.Review.ToString())
                {
                    string OrderId = response.OrderId;

                    string orderMessage = $"Order (Order id :{OrderId}) has been succesfully placed.";

                    request.body = orderMessage;

                    SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.ReOrderSuceessMessage);
                }
                else
                {
                    request.body = Constants.ReOrderFailureMessage;
                    SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.ReOrderFailureMessage);
                }

                return request;
            }
            catch
            {
                request.body = Constants.ApiExceptionMessage;
                SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.ApiExceptionMessage);
                return request;
            }
        }

        private IncomingRequest GetOrderStatus(IncomingRequest request, string orderOptionNumber, ChatRecord userlastRecordDb, string incomingMessage)
        {
            try
            {
                var url = "/getAllOrders";
               // var orders = WebGETSync<IList<OrderResponse>>(url);

                var orders = new List<OrderResponse>()
                           {
                            new OrderResponse{Name="Order1",Id=111111,ValidatedOn=DateTime.Now},
                            new OrderResponse{Name="Order2",Id=222222,ValidatedOn=DateTime.Now},
                            new OrderResponse{Name="Order3",Id=333333,ValidatedOn=DateTime.Now},
                            new OrderResponse{Name="Order4",Id=444444,ValidatedOn=DateTime.Now},
                            new OrderResponse{Name="Order5",Id=555555,ValidatedOn=DateTime.Now},
                           };

                string orderStatusUrl = "";
                string orderName = "";
                string orderId = "";

                if (orderOptionNumber == "1")
                {
                    orderStatusUrl = $"/getOrderStatusById?orderId={orders[0].Id}";

                    orderName = orders[0].Name;
                    orderId = orders[0].Id.ToString();
                }
                if (orderOptionNumber == "2")
                {
                    orderStatusUrl = $"/getOrderStatusById?orderId={orders[1].Id}";

                    orderName = orders[1].Name;
                    orderId = orders[1].Id.ToString();
                }
                if (orderOptionNumber == "3")
                {
                    orderStatusUrl = $"/getOrderStatusById?orderId={orders[2].Id}";

                    orderName = orders[2].Name;
                    orderId = orders[2].Id.ToString();
                }
                if (orderOptionNumber == "4")
                {
                    orderStatusUrl = $"/getOrderStatusById?orderId={orders[3].Id}";

                    orderName = orders[3].Name;
                    orderId = orders[3].Id.ToString();
                }
                if (orderOptionNumber == "5")
                {
                    orderStatusUrl = $"/getOrderStatusById?orderId={orders[4].Id}";

                    orderName = orders[4].Name;
                    orderId = orders[4].Id.ToString();
                }

               // var response = WebGETSync<OrderStatusResponse>(orderStatusUrl);
                var response = new OrderStatusResponse { Status = "LO" };

                if (response.Status == "LO")
                {

                    string orderStatus = $"Order {orderName} (Order id :{orderId}) has been locked." +
                                      $"\n Locked : Your order has been confirmed by the laboratory";

                    request.body = orderStatus;

                    SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.OrderStatusMessage);

                    return request;
                }

                if (response.Status == "OI")
                {

                    string orderStatus = $"Order {orderName} (Order id :{orderId}) has been imported by lab." +
                                         $"\n Imported : Your order has successfully reached laboratory";

                    request.body = orderStatus;

                    SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.OrderStatusMessage);

                    return request;
                }

                if (response.Status == "CV")
                {

                    string orderStatus = $"Order {orderName} (Order id :{orderId}) has been validated." +
                                         $"\n Validated: Your order is confirmed and yet to be received by laboratory.";

                    request.body = orderStatus;

                    SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.OrderStatusMessage);

                    return request;
                }

                else
                {
                    string orderStatus = $"Order {orderName} (Order id :{orderId}) is pending order." +
                                         $"\n Pending Order: Your order is not yet submitted and it is still in pending state.";

                    request.body = orderStatus;

                    SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.OrderStatusMessage);

                    request.body = orderStatus;

                    SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.ReOrderSuceessMessage);
                }

                return request;
            }
            catch
            {
                request.body = Constants.ApiExceptionMessage;
                SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.ApiExceptionMessage);
                return request;
            }
        }

        private IncomingRequest GetValidatedOrder(IncomingRequest request, ChatRecord userlastRecordDb, string incomingMessage)
        {
            try
            {
                //var response = WebGETSync<List<OrderResponse>>(url);

                var response = new List<OrderResponse>()
                           {
                            new OrderResponse{Name="Order1",Id=111111,ValidatedOn=DateTime.Now},
                            new OrderResponse{Name="Order2",Id=222222,ValidatedOn=DateTime.Now},
                            new OrderResponse{Name="Order3",Id=333333,ValidatedOn=DateTime.Now},
                            new OrderResponse{Name="Order4",Id=444444,ValidatedOn=DateTime.Now},
                            new OrderResponse{Name="Order5",Id=555555,ValidatedOn=DateTime.Now},
                           };

                var text = Constants.ValidatedOrdersMessage;

                string firstOrder = "";
                string secondOrder = "";
                string thirdOrder = "";
                string fourthOrder = "";
                string fifthOrder = "";

                if (response.Any())
                {
                    if (response[0] != null)
                    {
                        firstOrder = $"1.OrderName : {response[0].Name}, OrderId : {response[0].Id}, ValidatedOn : {response[0].ValidatedOn}";
                    }
                    if (response[1] != null)
                    {
                        secondOrder = $"2.OrderName : {response[1].Name}, OrderId : {response[1].Id}, ValidatedOn : {response[1].ValidatedOn}";
                    }
                    if (response[2] != null)
                    {
                        thirdOrder = $"3.OrderName : {response[2].Name}, OrderId : {response[2].Id}, ValidatedOn : {response[2].ValidatedOn}";
                    }
                    if (response[3] != null)
                    {
                        fourthOrder = $"4.OrderName : {response[3].Name}, OrderId : {response[3].Id}, ValidatedOn : {response[3].ValidatedOn}";
                    }
                    if (response[4] != null)
                    {
                        fifthOrder = $"5.OrderName : {response[4].Name}, OrderId : {response[4].Id}, ValidatedOn : {response[4].ValidatedOn}";
                    }

                    var ordersList = text + "@" + firstOrder + "@" + secondOrder + "@" + thirdOrder + "@" + fourthOrder + "@" + fifthOrder;
                    ordersList = ordersList.Replace("@", System.Environment.NewLine);

                    request.body = ordersList;

                    SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.ValidatedOrdersMessage);
                    return request;
                }
                else
                {
                    request.body = Constants.NoOrderMessage;
                    SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.NoOrderMessage);
                    return request;
                }
            }
            catch
            {
                request.body = Constants.ApiExceptionMessage;
                SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.ApiExceptionMessage);
                return request;
            }
        }

        private IncomingRequest GetAllOrders(IncomingRequest request, ChatRecord userlastRecordDb, string incomingMessage)
        {
            try
            {
                //var response = WebGETSync<List<OrderResponse>>(url);

                var response = new List<OrderResponse>()
                           {
                            new OrderResponse{Name="Order1",Id=111111,ValidatedOn=DateTime.Now},
                            new OrderResponse{Name="Order2",Id=222222,ValidatedOn=DateTime.Now},
                            new OrderResponse{Name="Order3",Id=333333,ValidatedOn=DateTime.Now},
                            new OrderResponse{Name="Order4",Id=444444,ValidatedOn=DateTime.Now},
                            new OrderResponse{Name="Order5",Id=555555,ValidatedOn=DateTime.Now},
                           };

                var text = Constants.AllOrdersMessage;

                string firstOrder = "";
                string secondOrder = "";
                string thirdOrder = "";
                string fourthOrder = "";
                string fifthOrder = "";

                if (response.Any())
                {
                    if (response[0] != null)
                    {
                        firstOrder = $"1.OrderName : {response[0].Name}, OrderId : {response[0].Id}, ValidatedOn : {response[0].ValidatedOn}";
                    }
                    if (response[1] != null)
                    {
                        secondOrder = $"2.OrderName : {response[1].Name}, OrderId : {response[1].Id}, ValidatedOn : {response[1].ValidatedOn}";
                    }
                    if (response[2] != null)
                    {
                        thirdOrder = $"3.OrderName : {response[2].Name}, OrderId : {response[2].Id}, ValidatedOn : {response[2].ValidatedOn}";
                    }
                    if (response[3] != null)
                    {
                        fourthOrder = $"4.OrderName : {response[3].Name}, OrderId : {response[3].Id}, ValidatedOn : {response[3].ValidatedOn}";
                    }
                    if (response[4] != null)
                    {
                        fifthOrder = $"5.OrderName : {response[4].Name}, OrderId : {response[4].Id}, ValidatedOn : {response[4].ValidatedOn}";
                    }

                    var ordersList = text + "@" + firstOrder + "@" + secondOrder + "@" + thirdOrder + "@" + fourthOrder + "@" + fifthOrder;
                    ordersList = ordersList.Replace("@", System.Environment.NewLine);

                    request.body = ordersList;

                    SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.AllOrdersMessage);
                    return request;
                }
                else
                {
                    request.body = Constants.NoOrderMessage;
                    SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.NoOrderMessage);
                    return request;
                }
            }
            catch
            {
                request.body = Constants.ApiExceptionMessage;
                SaveSentResponse(request, userlastRecordDb, incomingMessage, Constants.ApiExceptionMessage);
                return request;
            }
        }

        private IncomingRequest GetDropOffLocaction(IncomingRequest request)
        {
            var address = $"Here is your nearest drop off location. \n Norwich - Big YellowCanary Way, Norwich, NR1 1WY ,\n Eurofins Water Hygiene Testing UK -Wolverhampton , \n 4937.8 miles from search";

            request.body = address;

            return request;
        }

        private IncomingRequest NewReleaseNotes(IncomingRequest request)
        {
            request.body = Constants.ReleaseMessage;

            return request;
        }

        private IncomingRequest SuggestNewFeatures(IncomingRequest request)
        {
            request.body = Constants.ThanksForNewFeautresMessage;

            return request;
        }

        private IncomingRequest Feedback(IncomingRequest request)
        {
            request.body = Constants.ThanksForFeedbackMessage;

            return request;
        }
    }
}