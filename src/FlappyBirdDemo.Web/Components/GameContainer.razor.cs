﻿using FlappyBirdDemo.Core.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;

namespace FlappyBirdDemo.Web.Components
{
    public partial class GameContainer : IDisposable
    {
        [Inject] 
        private IGame GameManager { get; init; }

        protected override Task OnInitializedAsync()
        {
            GameManager.StateChanged += OnStateChanged;
            return base.OnInitializedAsync();
        }

        private void HandleKeyboard(KeyboardEventArgs e)
        {
            if (e.Key is not " ") 
                return;

            HandleClick();
        }

        private void HandleClick()
        {
            if (GameManager.IsRunning)
                GameManager.Jump();
            else GameManager.StartGame();
        }

        private void OnStateChanged(object sender, EventArgs e) => StateHasChanged();
        public void Dispose() => GameManager.StateChanged -= OnStateChanged;
    }
}