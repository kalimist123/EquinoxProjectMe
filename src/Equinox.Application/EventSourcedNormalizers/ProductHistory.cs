using System;
using System.Collections.Generic;
using System.Linq;
using Equinox.Domain.Core.Events;
using Newtonsoft.Json;

namespace Equinox.Application.EventSourcedNormalizers
{
    public class ProductHistory
    {
        public static IList<ProductHistoryData> HistoryData { get; set; }

        public static IList<ProductHistoryData> ToJavaScriptProductHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<ProductHistoryData>();
            ProductHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<ProductHistoryData>();
            var last = new ProductHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new ProductHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
                        ? ""
                        : change.Name,
                    Code = string.IsNullOrWhiteSpace(change.Code) || change.Code == last.Code
                        ? ""
                        : change.Code,
                    Category = string.IsNullOrWhiteSpace(change.Category) || change.Category == last.Category
                        ? ""
                        : change.Category,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void ProductHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new ProductHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "ProductRegisteredEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Category = values["category"];
                        slot.Code = values["code"];
                        slot.Name = values["Name"];
                        slot.Action = "Registered";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "ProductUpdatedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Category = values["Category"];
                        slot.Code = values["Code"];
                        slot.Name = values["Name"];
                        slot.Action = "Updated";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "ProductRemovedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Action = "Removed";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                }
                HistoryData.Add(slot);
            }
        }
    }
}