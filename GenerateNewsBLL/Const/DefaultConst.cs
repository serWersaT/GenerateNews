using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateNewsBLL.Const
{
    public  class DefaultConst
    {
        public string[] TypeWords = new string[] {
            "Выберите",
            "существительное",
            "главное существительное",
            "глагол",
            "наречие",
            "прилагательное",
            "числительное",
            "междометие",
            "частица",
            "местоимение",
            "предлог"
        };

        public string[] ThemeNews = new string[] {
            "Выберите",
            "Политика",
            "Культура",
            "Спорт",
            "Экономика",
            "Наука",
        };

        public string GetThemeNewsString(string number)
        {
            var result = "";
            switch (number)
            {
                case "1": result = "Политика"; break;
                case "2": result = "Культура"; break;
                case "3": result = "Спорт"; break;
                case "4": result = "Экономика"; break;
                case "5": result = "Наука"; break;
            }

            return result;
        }

        public int GetThemeNewsNumber(string type)
        {
            var result = -1;
            switch (type)
            {
                case "Политика": result = 1; break;
                case "Культура": result = 2; break;
                case "Спорт": result = 3; break;
                case "Экономика": result = 4; break;
                case "Наука": result = 5; break;
            }

            return result;
        }


        public int GetTypeWordNumber(string type)
        {
            var result = -1;
            switch (type)
            {
                case "существительное": result = 1; break;
                case "главное существительное": result = 2; break;
                case "глагол": result = 3; break;
                case "наречие": result = 4; break;
                case "прилагательное": result = 5; break;
                case "числительное": result = 6; break;
                case "междометие": result = 7; break;
                case "частица": result = 8; break;
                case "местоимение": result = 9; break;
                case "предлог": result = 10; break;
            }

            return result;
        }

        public string GetTypeWordString(int number)
        {
            var result = "";
            switch (number)
            {
                case 1: result = "существительное"; break;
                case 2: result = "главное существительное"; break;
                case 3: result = "глагол"; break;
                case 4: result = "наречие"; break;
                case 5: result = "прилагательное"; break;
                case 6: result = "числительное"; break;
                case 7: result = "междометие"; break;
                case 8: result = "частица"; break;
                case 9: result = "местоимение"; break;
                case 10: result = "предлог"; break;
            }

            return result;
        }
    }
}
