using FlappyBirdDemo.Core.Models;
using Microsoft.AspNetCore.Components;

namespace FlappyBirdDemo.Web.Components
{
    public partial class PipeComponent
    {
        [Parameter]
        public Pipe Pipe { get; set; }

        public string BottomStyle => Pipe is null 
            ? "display: none;"
            : $"height: {Pipe.Height}px; width: {Pipe.Width}px; left: {Pipe.PositionX}px; bottom: {Pipe.PositionY}px";

        public string TopStyle => Pipe is null
            ? "display: none;"
            : $"height: {Pipe.Height}px; width: {Pipe.Width}px; left: {Pipe.PositionX}px; bottom: {Pipe.PositionY + Pipe.Height + Pipe.Gap}px";
    }
}