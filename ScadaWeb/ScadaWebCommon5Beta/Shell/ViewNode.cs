﻿/*
 * Copyright 2016 Mikhail Shiryaev
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 * 
 * Product  : Rapid SCADA
 * Module   : ScadaWebCommon
 * Summary  : View tree node
 * 
 * Author   : Mikhail Shiryaev
 * Created  : 2016
 * Modified : 2016
 */

using Scada.Web.Plugins;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;

namespace Scada.Web.Shell
{
    /// <summary>
    /// View tree node
    /// <para>Узел дерева представлений</para>
    /// </summary>
    public class ViewNode : IWebTreeNode
    {
        /// <summary>
        /// Шаблон ссылки узла для загрузки представления
        /// </summary>
        protected const string ViewNodeUrlTemplate = "javascript:scada.masterMain.loadView({0}, \"{1}\");";


        /// <summary>
        /// Конструктор, ограничивающий создание объекта без параметров
        /// </summary>
        protected ViewNode()
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public ViewNode(ViewSettings.ViewItem viewItem, ViewSpec viewSpec)
        {
            if (viewItem == null)
                throw new ArgumentNullException("viewItem");

            ViewID = viewItem.ViewID;
            Text = viewItem.Text ?? "";
            AlarmCnlNum = viewItem.AlarmCnlNum;

            if (viewSpec == null)
            {
                Url = "";
                ViewUrl = "";
                IconUrl = "";
            }
            else
            {
                ViewUrl = VirtualPathUtility.ToAbsolute(viewSpec.GetViewUrl(ViewID));
                Url = string.Format(ViewNodeUrlTemplate, ViewID, ViewUrl);
                IconUrl = VirtualPathUtility.ToAbsolute(viewSpec.IconUrl);
            }

            Level = -1;
            ChildNodes = new List<ViewNode>();
            InitDataAttrs();
        }


        /// <summary>
        /// Получить или установить идентификатор представления
        /// </summary>
        public int ViewID { get; set; }

        /// <summary>
        /// Получить текст
        /// </summary>
        public string Text { get; protected set; }

        /// <summary>
        /// Получить номер входного канала, информирующего о тревожном состоянии представления
        /// </summary>
        public int AlarmCnlNum { get; protected set; }

        /// <summary>
        /// Получить ссылку узла
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// Получить ссылку представления
        /// </summary>
        public string ViewUrl { get; protected set; }

        /// <summary>
        /// Получить ссылку на иконку
        /// </summary>
        public string IconUrl { get; protected set; }

        /// <summary>
        /// Получить или установить уровень вложенности
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Получить дочерние узлы
        /// </summary>
        public List<ViewNode> ChildNodes { get; protected set; }

        /// <summary>
        /// Получить дочерние узлы
        /// </summary>
        public IList Children
        {
            get
            {
                return ChildNodes;
            }
        }

        /// <summary>
        /// Получить атрибуты данных в виде пар "имя-значение"
        /// </summary>
        public SortedList<string, string> DataAttrs { get; protected set; }


        /// <summary>
        /// Инициализировать атрибуты данных
        /// </summary>
        protected void InitDataAttrs()
        {
            DataAttrs = new SortedList<string, string>();
            DataAttrs.Add("cnl", AlarmCnlNum.ToString());
        }
        
        /// <summary>
        /// Определить, что узел соответствует выбранному объекту
        /// </summary>
        public bool IsSelected(object selObj)
        {
            return false;
        }
    }
}
