using System;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.IO;

namespace System.Windows
{
    public static class UIATaskWorker
    {
        public static void ExecuteTask(UIAActionItem[] tasks, FrameworkElement fe)
        {
            if(tasks != null)
            {
                foreach(UIAActionItem item in tasks)
                {
                    Window w = FindWindow(item.s_window);
                    w.Dispatcher.Invoke(() => {
                        
                        FrameworkElement element = w.FindName(item.s_fe) as FrameworkElement;
                        //var element = (dynamic)Convert.ChangeType(fe,item.s_type);
                        if(item.ait)
                        {
                            Type elementType = element.GetType();
                            if(elementType == typeof(System.Windows.Controls.TextBox))
                            {
                                PropertyInfo textProperty = elementType.GetProperty("Text");
                                if (textProperty != null)
                                {
                                    textProperty.SetValue(element, ((string)fe.AIResponse));
                                }
                            } else if(elementType == typeof(System.Windows.Controls.Image))
                            {
                                PropertyInfo sourceProperty = elementType.GetProperty("Source");
                                string imageBytes = ((OpenAI.Images.GeneratedImage)fe.AIResponse).ImageUri.AbsoluteUri;

                                // Create a new BitmapImage
                                BitmapImage bitmap = new BitmapImage();
                                bitmap.BeginInit();
                                bitmap.UriSource = new Uri(imageBytes);
                                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                                bitmap.EndInit();

                                // Set the Image control's Source property
                                sourceProperty.SetValue(element, bitmap);
                                
                            } else if(elementType == typeof(System.Windows.Controls.Label))
                            {
                                PropertyInfo textProperty = elementType.GetProperty("Content");
                                if (textProperty != null)
                                {
                                    textProperty.SetValue(element, ((string)fe.AIResponse));
                                }
                            }
                        } else{
                            element.RaiseEvent(item.s_e);
                        }
                    });
                }
            }
        }

        private static Window FindWindow(string windowName)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Name == windowName)
                {
                    return window;
                }
            }
            return null;
        }
    }

}