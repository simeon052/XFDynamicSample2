using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Reflection;

namespace XFDynamicSample2.Views
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            pressedCommand = new PressedCommand();
            pressedCommand.Page = this;
            pressed2Command = new Pressed2Command();
            pressed2Command.Page = this;

            this.TextStr = "Something";



            var statckLayout = new StackLayout
            {
                Children = {
                    new Button { Text = "Button", Command=pressedCommand },
                }
            };

            this.BindingContext = this;
            var label = new Label();// { Text = TextStr };
            label.SetBinding(Label.TextProperty, nameof(TextStr));

            statckLayout.Children.Add(label);
            var button2 = new Button { Text = "Button 2", Command = pressed2Command };
            statckLayout.Children.Add(button2);
            

            Content = statckLayout;
        }

        private BindableProperty textStrProperty = BindableProperty.Create(
            nameof(TextStr),
            typeof(string),
            typeof(MainPage),
            "init",
            BindingMode.OneWay,
            null,
            null,
            null,
            null);

        public string TextStr { get { return this.GetValue(textStrProperty) as string; } set
            {
                this.SetValue(textStrProperty, value);
            }
        }


        private PressedCommand pressedCommand { get; set; }
        private Pressed2Command pressed2Command { get; set; }


        private class PressedCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public MainPage Page { get; set; }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                System.Diagnostics.Debug.WriteLine("pressed");
                Page.TextStr = "Pressed";
            }
        }

        private class Pressed2Command : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public MainPage Page { get; set; }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                var stack = Page.Content as StackLayout;
                stack.Children.Add(new Button { Text = "Added button" });

                // object d = DayOftheWeek.Sunday;
                object d = "string" as string;

                bool isEnum = false;

#if WINDOWS_UWP
                isEnum = d.GetType().GetTypeInfo().IsEnum;
#else                
                isEnum = d.GetType().IsEnum;
#endif

                if (isEnum)
                {
                    // Pickerを生成する
                    var picker = new Picker
                    {
                        Title = "選択してください",
                        VerticalOptions = LayoutOptions.Start //  上詰めで配置する（縦方向）
                    };

                    foreach (var a in Enum.GetValues(d.GetType()))
                    {
                        picker.Items.Add(a.ToString());
                        picker.SelectedIndexChanged += ((s, e) =>
                        {
                            d = Convert.ChangeType(Enum.Parse(d.GetType(), picker.Items[picker.SelectedIndex]), d.GetType());
                            System.Diagnostics.Debug.WriteLine(d.ToString());
                        });

                    }
                    stack.Children.Add(picker);
                }
                else
                {
                    var entry = new Entry { Text = d.ToString() };
                    entry.TextChanged += ((s, e) =>
                    {
                        d = entry.Text;
                        System.Diagnostics.Debug.WriteLine(d.ToString());

                    });
                    stack.Children.Add(entry);
                    


                }

            }
        }

    }

    public enum DayOftheWeek
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Firday,
        SatureDay,
    }
}