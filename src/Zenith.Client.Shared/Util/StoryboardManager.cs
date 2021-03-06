﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Zenith.Client.Shared.Util
{
    public class StoryboardManager
    {
        public static DependencyProperty IDProperty =
            DependencyProperty.RegisterAttached("ID", typeof(string), typeof(StoryboardManager),
                    new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender, IDChanged));
                    //new FrameworkPropertyMetadata(
                    //    "",
                    //    FrameworkPropertyMetadataOptions.AffectsRender
                    //    IDChanged));

        static Dictionary<string, Storyboard> _storyboards = new Dictionary<string, Storyboard>();

        private static void IDChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Storyboard sb = sender as Storyboard;
            if (sb == null)
                return;

            string key = e.NewValue as string;
            if (_storyboards.ContainsKey(key))
                _storyboards[key] = sb;
            else
                _storyboards.Add(key, sb);
        }

        public static void PlayStoryboard(string id, Callback callback, object state)
        {
            if (!_storyboards.ContainsKey(id))
            {
                callback(state);
                return;
            }

            Storyboard sb = _storyboards[id];
            EventHandler handler = null;
            handler = delegate { sb.Completed -= handler; callback(state); };
            sb.Completed += handler;
            sb.Begin();
        }

        public static void SetID(DependencyObject obj, string id)
        {
            obj.SetValue(IDProperty, id);
        }

        public static string GetID(DependencyObject obj)
        {
            return obj.GetValue(IDProperty) as string;
        }
    }

    public delegate void Callback(object state);
}
