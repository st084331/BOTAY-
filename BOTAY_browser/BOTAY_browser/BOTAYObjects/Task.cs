using System;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

namespace BOTAY_browser
{
    public class Task
    {
        private string _Name = "NoName";
        private string _Url = "";
        private string _FullName = "";
        private string _Deadline = "NoDeadline";
        private bool _IsReady = false;

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                //Защита от пустых названий и пробелов
                if (!String.IsNullOrWhiteSpace(value) && !String.IsNullOrEmpty(value))
                {
                    _Name = value;
                }
                else
                {
                    _Name = "NoName";
                }
            }
        }



        public string Url
        {
            get
            {
                return _Url;
            }
            set
            {
                //Проверка ссылки на валидность
                if (IsUrlValid(value))
                {
                    _Url = value;
                }
                else
                {
                    _Url = string.Empty;
                }
            }
        }
        public bool IsReady
        {
            get { return _IsReady; }
            set
            {
                _IsReady = value;
            }
        }
        public string FullName
        {
            get { return _FullName; }
            set
            {
                //Защита от пустых строк и пробелов
                if (!String.IsNullOrWhiteSpace(value) && !String.IsNullOrEmpty(value))
                {
                    _FullName = value;
                }
                else
                {
                    _FullName = string.Empty;
                }
            }
        }
        public string Deadline
        {
            get { return _Deadline; }
            set
            {
                var culture = CultureInfo.GetCultureInfo("ru");
                if (value != "")
                {
                    try
                    {
                        //Если дата корректна и она не раньше текущего дня, то валидно
                        DateTime date = DateTime.Parse(value, culture);
                        if (DateTime.Compare(date, DateTime.Today) >= 0)
                        {
                            _Deadline = date.Date.ToString("d", culture);
                        }
                    }
                    catch
                    {
                        _Deadline = "NoDeadline";
                    }
                }
                else
                {
                    _Deadline = "NoDeadline";
                }
            }
        }

        public Task()
        {
        }

        private bool IsUrlValid(string url)
        {
            return Regex.IsMatch(url, @"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        }


    }
}
