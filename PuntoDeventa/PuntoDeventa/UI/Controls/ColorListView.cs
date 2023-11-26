using Xamarin.Forms;

namespace PuntoDeventa.UI.Controls
{
    public sealed class ColorListView : ListView
    {
        protected override void SetupContent(Cell content, int index)
        {
            if (content is ViewCell viewCell)
                viewCell.View.BackgroundColor = index % 2 == 0 ? Color.FromHex("E7E6E6") : Color.White;

            base.SetupContent(content, index);

        }
    }
}
