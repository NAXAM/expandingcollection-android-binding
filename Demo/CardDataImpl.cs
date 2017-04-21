using System.Collections.Generic;
using Com.Ramotion.Expandingcollection;
using Java.Lang;
using System.Collections;

namespace Demo
{
    public class CardDataImpl : Java.Lang.Object, IECCardData
    {
        public Integer HeadBackgroundResource { get; set; }

        public Integer MainBackgroundResource { get; set; }

        public IList ListItems { get; set; }

        public string CardTitle { get; set; }

        public static IList<IECCardData> GenerateExampleData()
        {
            IList<IECCardData> list = new List<IECCardData>();
            var res = new int[] {
                Resource.Drawable.attractions,
                Resource.Drawable.attractions_head,
                Resource.Drawable.city_scape,
                Resource.Drawable.city_scape_head,
                Resource.Drawable.nature,
                Resource.Drawable.nature_head
            };

            for (int i = 0; i < 3; i++)
            {
                var resIndex = i % 3;
                var title = $"Card {i + 1}";
                list.Add(new CardDataImpl {
                    HeadBackgroundResource = (Integer) res[resIndex * 2],
                    MainBackgroundResource = (Integer)res[resIndex * 2 + 1],
                    CardTitle = title,
                    ListItems = CreateItemsList(title)
                });
            }
            return list;
        }

        private static IList CreateItemsList(string cardName)
        {
            return new List<string> {
                cardName + " - Item 1",
                cardName + " - Item 2",
                cardName + " - Item 3",
                cardName + " - Item 4",
                cardName + " - Item 5",
                cardName + " - Item 6",
                cardName + " - Item 7"
            };
        }
    }
}