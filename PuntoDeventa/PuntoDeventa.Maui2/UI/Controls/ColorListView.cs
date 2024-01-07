using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.UI.Controls
{
    public sealed class ColorListView : ListView
    {
        protected override void SetupContent(Cell content, int index)
        {
            if (content is ViewCell viewCell)
                viewCell.View.BackgroundColor = index % 2 == 0 ? Color.FromArgb("E7E6E6") : Colors.White;

            base.SetupContent(content, index);

        }
    }
}
