using Microsoft.AspNetCore.Components;

namespace FlappyBirdDemo.Web.Components
{
    public partial class ScoreComponent
    {
        [Parameter]
        public int Score { get; set; }
    }
}