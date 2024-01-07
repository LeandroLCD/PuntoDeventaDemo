using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.UI.Controls.Trigger
{
    public class FadeToTrigger : TriggerAction<VisualElement>
    {
        public uint Duration { get; set; }
        public double To { get; set; }
        protected override async void Invoke(VisualElement sender)
        {
            await sender.FadeTo(To, Duration, Easing.Linear);
        }
    }
}
