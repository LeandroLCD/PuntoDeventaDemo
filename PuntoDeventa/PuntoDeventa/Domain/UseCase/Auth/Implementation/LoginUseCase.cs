﻿using PuntoDeventa.Data.Repository.Auth;
using PuntoDeventa.UI.Auth.Models;
using PuntoDeventa.UI.Auth.States;
using System.Threading.Tasks;

namespace PuntoDeventa.Domain.UseCase.Auth.Implementation
{
    internal class LoginUseCase : BaseAuthUseCase, ILoginUseCase
    {
        private IAuthRepository _repository;

        public LoginUseCase(IAuthRepository authRepository)
        {
            _repository = authRepository;

        }

        public async Task<AuthStates> Login(AuthDataUser model)
        {
            return await MakeCallUseCase(model, async () =>
            {
                return await _repository.Login(model.Email, model.Password);
            });
        }
    }
}
