using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenerateNewsDAL.Models;
using GenerateNewsDAL.Interfaces;
using GenerateNewsDAL.DB;

namespace GenerateNewsBLL.Services
{
    public class Service: DefaultService
    {       
        DownLevelService downservice = new DownLevelService();

        #region Word
        //public int WordCnt()
        //{
        //    return FuncCount<WordModel>(dw);
        //}

        //public bool InsertWord(WordModel model)
        //{
        //    FuncInsert<WordModel>(dw, model);
        //    return true;
        //}

        //public bool UpdateWord(WordModel model)
        //{
        //    FuncUpdate<WordModel>(dw, model);
        //    return true;
        //}

        //public List<WordModel> Select(int? topmin, int? topmax)
        //{
        //    return FuncSelect<WordModel>(dw, topmin, topmax);
        //}
        #endregion


        #region Phrase
        //public int PhraseCnt()
        //{
        //    return FuncCount<PhraseModel>(dp);
        //}

        //public bool InsertPhrase(PhraseModel model)
        //{
        //    FuncInsert<PhraseModel>(dp, model);
        //    return true;
        //}

        //public bool UpdatePhrase(PhraseModel model)
        //{
        //    FuncUpdate<PhraseModel>(dp, model);
        //    return true;
        //}

        //public List<PhraseModel> SelectPhrase(int? topmin, int? topmax)
        //{
        //    return FuncSelect<PhraseModel>(dp, topmin, topmax);
        //}
        #endregion

        #region News
        //public int NewsCnt()
        //{
        //    return FuncCount<NewsModel>(dn);
        //}

        //public bool InsertNews(NewsModel model)
        //{
        //    FuncInsert<NewsModel>(dn, model);
        //    return true;
        //}

        //public bool UpdateNews(NewsModel model)
        //{
        //    FuncUpdate<NewsModel>(dn, model);
        //    return true;
        //}

        //public List<NewsModel> SelectNews(int? topmin, int? topmax)
        //{
        //    return FuncSelect<NewsModel>(dn, topmin, topmax);
        //}
        #endregion

        #region ConfigWord
        //public int ConfigWordCnt()
        //{
        //    return FuncCount<ConfigWordModel>(cw);
        //}

        //public bool InsertConfigWord(ConfigWordModel model)
        //{
        //    FuncInsert<ConfigWordModel>(cw, model);
        //    return true;
        //}

        //public bool UpdateConfigWord(ConfigWordModel model)
        //{
        //    FuncUpdate<ConfigWordModel>(cw, model);
        //    return true;
        //}

        //public List<ConfigWordModel> SelectConfigWord(int? topmin, int? topmax)
        //{
        //    return FuncSelect<ConfigWordModel>(cw, topmin, topmax);
        //}

        //public List<ConfigWordModel> FindConfigWord(ConfigWordModel model, int topmax)
        //{
        //    var sql = " a.Active = 1 ";
        //    if (!string.IsNullOrEmpty(model.EmotionLevel.ToString())) sql += " and a.EmotionLevel = " + model.EmotionLevel.ToString() + " ";
        //    if (!string.IsNullOrEmpty(model.FormWord)) sql += " and a.FormWord like '%" + model.FormWord.ToString() + "%' ";
        //    if (!string.IsNullOrEmpty(model.GenderForm)) sql += " and a.GenderForm like '%" + model.GenderForm.ToString() + "%' ";
        //    if (!string.IsNullOrEmpty(model.Mix)) sql += " and a.Mix like '%" + model.Mix.ToString() + "%' ";
        //    if (!string.IsNullOrEmpty(model.MixFormWord)) sql += " and a.MixFormWord like '%" + model.MixFormWord.ToString() + "%' ";
        //    if (!string.IsNullOrEmpty(model.MixGenderForm)) sql += " and a.MixGenderForm like '%" + model.MixGenderForm.ToString() + "%' ";
        //    if (!string.IsNullOrEmpty(model.MixTypeWord)) sql += " and a.MixTypeWord like '%" + model.MixTypeWord.ToString() + "%' ";
        //    if (!string.IsNullOrEmpty(model.NotMixFormWord)) sql += " and a.NotMixFormWord like '%" + model.NotMixFormWord.ToString() + "%' ";
        //    if (!string.IsNullOrEmpty(model.NotMixGenderForm)) sql += " and a.NotMixGenderForm like'%" + model.NotMixGenderForm.ToString() + "%' ";
        //    if (!string.IsNullOrEmpty(model.NotMixTypeWord)) sql += " and a.NotMixTypeWord like '%" + model.NotMixTypeWord.ToString() + "%' ";
        //    if (!string.IsNullOrEmpty(model.Not_Mix)) sql += " and a.Not_Mix like '%" + model.Not_Mix.ToString() + "%' ";
        //    if (model.RowNum != 0) sql += " and RowNum = " + model.RowNum.ToString() + " ";
        //    if (!string.IsNullOrEmpty(model.str)) sql += " and a.str like '%" + model.str.ToString() + "%' ";
        //    if (!string.IsNullOrEmpty(model.strContains)) sql += " and a.strContains like '%" + model.strContains.ToString() + "%' ";
        //    if (!string.IsNullOrEmpty(model.strMixContains)) sql += " and a.strMixContains like '%" + model.strMixContains.ToString() + "%' ";
        //    if (!string.IsNullOrEmpty(model.strNotMixContains)) sql += " and a.strNotMixContains like '%" + model.strNotMixContains.ToString() + "%' ";
        //    if (!string.IsNullOrEmpty(model.TypeWord)) sql += " and a.TypeWord like '%" + model.TypeWord.ToString() + "%' ";
        //    //sql += " ORDER BY Id OFFSET 0 ROWS FETCH NEXT " + topmax.ToString() + " ROWS ONLY";
        //    return FuncSelectWhere<ConfigWordModel>(cw, sql);
        //}
        #endregion

        #region ConfigPhrase
        //public int ConfigPhraseCnt()
        //{
        //    return FuncCount<ConfigPhraseModel>(cp);
        //}

        //public bool InsertConfigPhrase(ConfigPhraseModel model)
        //{
        //    FuncInsert<ConfigPhraseModel>(cp, model);
        //    return true;
        //}

        //public bool UpdateConfigPhrase(ConfigPhraseModel model)
        //{
        //    FuncUpdate<ConfigPhraseModel>(cp, model);
        //    return true;
        //}

        //public List<ConfigPhraseModel> SelectConfigPhrase(int? topmin, int? topmax)
        //{
        //    return FuncSelect<ConfigPhraseModel>(cp, topmin, topmax);
        //}
        #endregion

        #region Index


        //public string GeneratePhrase(string flagMix)
        //{
        //    var result = "";
        //    var vectorphrase = "";
        //    var word = "";
        //    var emlevel = new List<int>();
        //    var theme = downservice.GetPhraseTheme();
        //    var AllSchemes = downservice.GetVector("cхема");
        //    if (AllSchemes.Count() <= 0)
        //    {
        //        AllSchemes.Add(downservice.GenerateScheme());
        //        //return "В программу не загружена ни одна схема предложения. Вначале выполните обучение";
        //    }
        //    int val = downservice.GetRandomCount(0, AllSchemes.Count());

        //    var wordconf = FuncSelect<ConfigWordModel>(cw, 0, FuncCount<ConfigWordModel>(cw));
        //    string[] masScheme = AllSchemes[val].Split('-');

        //    foreach (var x in masScheme)
        //    {
        //        var typeword = consts.GetTypeWordString(Convert.ToInt32(x));
        //        var sql = " Active = 1 and Type = '" + typeword + "' ";

        //        sql += downservice.GetConfigWordSql(wordconf, flagMix);

        //        var maswords = FuncSelectWhere<WordModel>(dw, sql).ToArray();

        //        if (maswords.Count() == 0)
        //        {
        //            sql = " Active = 1 and Type = '" + typeword + "' ";
        //            maswords = FuncSelectWhere<WordModel>(dw, sql).ToArray();
        //        }

        //        int v =(maswords.Count()==0)? downservice.GetRandomCount(0, maswords.Count()):1; 
        //        word = maswords[v].Word;
        //        result += word + " ";
        //        if (!string.IsNullOrEmpty(maswords[v].EmotionLevel.ToString())) { emlevel.Add((int)maswords[v].EmotionLevel); }
        //        vectorphrase += maswords[v].Id + "-";
        //    }

        //    PhraseModel model = new PhraseModel()
        //    {
        //        Active = 1,
        //        Phrase = result,
        //        EmotionLevel = (emlevel.Count() > 0) ? emlevel.Max() : 0,
        //        Theme = theme
        //    };

        //    FuncInsert<PhraseModel>(dp, model);

        //    return result;
        //}


        //public string GenerateNews(string ThemeNews, bool newvector, bool newphrases)
        //{
        //    //newvector - разрешить генерацию нового вектора для создания фраз
        //    //newphrases - разрешить генерацию новой фразы или приоритетено брать из таблицы
        //    string result = "";

        //    //Анализ текстаов 
        //    var sql = " Active = 1 and Theme = '" + ThemeNews + "' ";
        //    var AllNews = FuncSelectWhere<NewsModel>(dn, sql).ToList();
        //    var wordconf = FuncSelect<ConfigWordModel>(cw, 0, FuncCount<ConfigWordModel>(cw));
        //    var allword = "";
        //    foreach (var news in AllNews){ allword += news; }

        //    var listword = allword.Split(' ', '.', '!', ',', '?', '%', '"');
        //    var AnalizWord = downservice.GetAnalizeWords(listword);

        //    sql = " Active = 1 and LevelVector = 'схема' ";            
        //    var vectors = FuncSelectWhere<VectorModel>(vt, sql);
            
        //    var listThemes = AnalizWord.Where(x => x.Type == "главное существительное").OrderBy(z => z.weight).ToArray();

        //    int v = downservice.GetRandomCount(0, listThemes.Count()); 
        //    var FirstTheme = listThemes[v].Word;

        //    var vector = new List<string>();
        //    if (newvector == true)
        //    {
        //        vector = downservice.GetVector();

        //        while (vector.Where(x => x.Contains("существительное")).Count() < 2)
        //        {
        //            vector = downservice.GetVector();
        //        }
        //    }
        //    else
        //    {
        //        if (vectors.Count() < 1) return "Не найдена ни одна схема предложения. В начале выполните обучение программы.";
        //        v = downservice.GetRandomCount(0, vectors.Count());
        //        var scheme = vectors[v].Vector.Split('-');
        //        foreach (var x in scheme)
        //        {
        //            vector.Add(consts.GetTypeWordString(Convert.ToInt32(x)));
        //        }

        //    }

        //        int cntGeneralWord = vector.Where(x => x.Contains("главное существительное")).Count();
        //        while (cntGeneralWord == 1)
        //        {
        //            if (cntGeneralWord == 0)
        //            {
        //                for (int i = 0; i < vector.Count(); i++)
        //                {
        //                    if (vector[i] == "существительное")
        //                    {
        //                        vector[i] = "главное существительное";
        //                        break;
        //                    }
        //                }
        //            }
        //            if (cntGeneralWord > 1)
        //            {
        //                bool firstGeneralWord = false;
        //                for (int i = 0; i < vector.Count(); i++)
        //                {
        //                    if (vector[i] == "главное существительное" && firstGeneralWord == true)
        //                    {
        //                        vector[i] = "существительное";
        //                        break;
        //                    }
        //                    if (vector[i] == "главное существительное")
        //                    {
        //                        firstGeneralWord = true;
        //                    }
        //                }
        //            }
        //        }

        //    var cntsentence = downservice.GetRandomCount(10, 45);
        //    var theme = downservice.GetGeneralWord();
        //    for (int i = 0; i < cntsentence; i++)
        //    {
        //        if (newphrases == true)
        //        {
        //            foreach (var typeword in vector)
        //            {
        //                var words = AnalizWord.Where(x => x.Type == typeword);
        //                var cntmaxword = words.Count();
        //                var cntword = downservice.GetRandomCount(0, cntmaxword);
        //                if (typeword == "главное существительное") sql = " Active = 1 and Word = '" + theme + "' ";
        //                else sql = " Active = 1 and Type = '" + typeword + "' ";

        //                var word = FuncSelectWhere<WordModel>(dw, sql);
        //                sql += downservice.GetConfigWordSql(wordconf, "on");
        //                var AllWords = FuncSelectWhere<WordModel>(dw, sql);
        //                var rmd = downservice.GetRandomCount(0, AllWords.Count());
        //                result += AllWords[rmd].Word + " ";
        //                if (typeword == "существительное") theme = AllWords[rmd].Word;
        //            }
        //            result = result.Trim(' ') + ". ";
        //        }
        //        else
        //        {
        //            sql = " Active = 1 and phrase like '%" + theme + "%'";
        //            var allphrase = FuncSelectWhere<PhraseModel>(dp, sql);
        //            var cntphr = downservice.GetRandomCount(0, allphrase.Count());
        //            var phrase = allphrase[cntphr].Phrase;
        //            foreach (var s in phrase.Split(' '))
        //            {
        //                sql = " Word = '" + s + "' ";
        //                var word = FuncSelectWhere<WordModel>(dw, sql).FirstOrDefault();
        //                if (word.Type == "существительное") theme = word.Word;
        //            }
        //            result += phrase + ". ";
        //        }
        //    }
        //    return result.Trim(' ');
        //}

        //public bool LearnText(NewsModel model)
        //{
        //    var configWord = new List<ConfigWordModel>();
        //    configWord = SelectConfigWord(0, ConfigWordCnt());     //список правил для определения слов 

        //    var configPhrase = new List<ConfigPhraseModel>();
        //    configPhrase = SelectConfigPhrase(0, ConfigPhraseCnt());     //список правил для определения фраз 

        //    NewsModel news = new NewsModel()
        //    {
        //        Active = 1,
        //        Created = DateTime.Now,
        //        News = model.News,
        //        EmotionLevel = model.EmotionLevel,
        //        Theme = model.Theme
        //    };
        //    InsertNews(news);
        //    try
        //    {
        //        var ListPhrases = model.News.Split('.','!','?',';');
        //        foreach (var lp in ListPhrases)
        //        {
        //            PhraseModel phrase = new PhraseModel();
        //            phrase.Phrase = lp.ToLower().Replace(",","");
        //            phrase.Active = 1;
        //            foreach(var conf in configPhrase)
        //            {
        //                if(lp.Contains(conf.Word))
        //                {
        //                    phrase.Theme = conf.Theme;
        //                    phrase.EmotionLevel = conf.EmotionLevel;
        //                }
        //            }
        //            InsertPhrase(phrase);

        //            var ListWord = lp.ToLower().Split(',', ' ');
        //            int i = 0;
        //            foreach (var lw in ListWord)
        //            {
        //                WordModel word = new WordModel();
        //                word.Active = 1;
        //                word.Word = lw;
        //                word.EmotionLevel = 0;                        

        //                if (i < ListWord.Count() - 1)
        //                {
        //                    var sql = "Active = 1 and Word = '" + lw + "' and Mix not like '%" + ListWord[i + 1] + "%' ";
        //                    var getmass = FuncSelectWhere<WordModel>(dw, sql).ToList();
        //                    string getword = "";
        //                    if (getmass.Count() > 0) getword = getmass.FirstOrDefault().Mix;
        //                    if (i + 1 < ListWord.Count())
        //                    {
        //                        word.Mix += ListWord[i + 1] + ";";
        //                    }
        //                }
        //                var qwe = lw;
        //                foreach (var conf in configWord)
        //                {
        //                    switch (conf.strContains)
        //                    {
        //                        case "начинается":
        //                            if (lw.StartsWith(conf.str) && conf.TypeWord == consts.GetTypeWordString(2))
        //                            {
        //                                goto case "default";
        //                            }
        //                            break;
        //                        case "содержит":
        //                            if (lw.Contains(conf.str) /*&& conf.TypeWord == consts.GetTypeWordString(2)*/)
        //                            {
        //                                goto case "default";
        //                            }
        //                            break;
        //                        case "равно":
        //                            if (lw == conf.str /*&& conf.TypeWord == consts.GetTypeWordString(2)*/)
        //                            {
        //                                goto case "default";
        //                            }
        //                            break;
        //                        case "заканчивается":
        //                            var eee = consts.GetTypeWordString(2);
        //                            if (lw.EndsWith(conf.str) /*&& conf.TypeWord == consts.GetTypeWordString(2)*/)
        //                            {
        //                                goto case "default";
        //                            }
        //                            break;
        //                        case "default":
        //                            word.EmotionLevel = (string.IsNullOrEmpty(conf.EmotionLevel.ToString())) ? 0 : conf.EmotionLevel;
        //                            word.FormWord = conf.FormWord;
        //                            word.Gender_Type = conf.GenderForm;
        //                            word.Mix += (string.IsNullOrEmpty(conf.Mix)) ? "" : conf.Mix + ";";
        //                            word.Not_Mix += (string.IsNullOrEmpty(conf.Not_Mix)) ? "" : conf.Not_Mix + ";";
        //                            word.Type = conf.TypeWord;
        //                            break;
        //                    }
        //                }
        //                if (string.IsNullOrEmpty(word.Type) || word.Type == consts.GetTypeWordString(1))
        //                {
        //                    string sql = " Active = 1 and TypeWord = '" + consts.GetTypeWordString(2) + "' ";
        //                    var confWord = FuncSelectWhere<ConfigWordModel>(cw, sql);
        //                    word.Type = consts.GetTypeWordString(1);
        //                    foreach (var types in confWord)
        //                    {
        //                        switch (types.strContains)
        //                        {
        //                            case "начинается":
        //                                if (lw.StartsWith(types.str))
        //                                {
        //                                    goto case "default";
        //                                }
        //                                break;
        //                            case "содержит":
        //                                if (lw.Contains(types.str))
        //                                {
        //                                    goto case "default";
        //                                }
        //                                break;
        //                            case "равно":
        //                                if (lw == types.str)
        //                                {
        //                                    goto case "default";
        //                                }
        //                                break;
        //                            case "заканчивается":
        //                                if (lw.EndsWith(types.str))
        //                                {
        //                                    goto case "default";
        //                                }
        //                                break;
        //                            case "default":
        //                                word.EmotionLevel = (string.IsNullOrEmpty(types.EmotionLevel.ToString())) ? 0 : types.EmotionLevel;
        //                                word.FormWord = types.FormWord;
        //                                word.Gender_Type = types.GenderForm;
        //                                word.Mix += (string.IsNullOrEmpty(types.Mix)) ? "" : types.Mix + ";";
        //                                word.Not_Mix += (string.IsNullOrEmpty(types.Not_Mix)) ? "" : types.Not_Mix + ";";
        //                                word.Type = types.TypeWord;
        //                                break;
        //                        }
        //                    }
        //                }
        //                i++;
        //                InsertWord(word);
        //            }

        //        }

        //        return true;
        //    }

        //    catch(Exception ex)
        //    {
        //        return false;
        //    }
        //}
        #endregion

        public string[] GetTypeWords()
        {
            return consts.TypeWords;
        }

        public string[] GetThemesNews()
        {
            return consts.ThemeNews;
        }
    }
}
