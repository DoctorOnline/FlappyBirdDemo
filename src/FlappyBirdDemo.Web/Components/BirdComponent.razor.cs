using FlappyBirdDemo.Core.Models;
using Microsoft.AspNetCore.Components;

namespace FlappyBirdDemo.Web.Components
{
    public partial class BirdComponent
    {
        [Parameter]
        public Bird Bird { get; set; }

        private string Style => Bird is null 
                ? "display: none;" 
                : $"height: {Bird.Height}px; width: {Bird.Width}px; bottom: {Bird.PositionY}px; left: {Bird.PositionX}px;";
    }
}