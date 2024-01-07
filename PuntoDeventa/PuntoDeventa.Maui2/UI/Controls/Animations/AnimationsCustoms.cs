using System;
using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.UI.Controls.Animations
{
    public static class AnimationsCustoms
    {

        public static Grid ClearAllAnimate(this Grid grid)
        {

            // Crea una lista para almacenar las animaciones de traslación
            var translationAnimations = grid.Children.ToList()
                .Select(child => new Animation((progress) =>
                {
                    child.TranslationX = Math.Cos(Math.PI / 4) * grid.Width * progress;
                    child.TranslationY = Math.Sin(Math.PI / 4) * grid.Height * progress;
                    child.Opacity = 1 - progress; // Ajusta la opacidad si deseas que los elementos se desvanezcan
                }, 0, 1))
                .ToList();


            // Crea una animación global para eliminar todos los hijos del Grid
            var globalAnimation = new Animation();

            // Agrega cada animación de traslación a la animación global
            foreach (var translationAnimation in translationAnimations)
            {
                globalAnimation.Add(0, 1, translationAnimation);
            }

            // Configura la duración y el tipo de interpolación para la animación global
            globalAnimation.Commit(grid, "RemoveChildrenAnimation", length: 500, easing: Easing.Linear,
                finished: (d, b) =>
                {
                    // Elimina todos los hijos del Grid después de la animación
                    grid.Children.Clear();
                });

            return grid;
        }
    }
}
