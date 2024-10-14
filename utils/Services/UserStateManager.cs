using CRM.Models;
using CRM.Entidades;
using System.Collections.Concurrent;

namespace CRM.Services
{
    public class UserStateManager
    {
        private Usuario _currentUser; // Armazena o usuário atual

        // Retorna o usuário atual ou null se não houver
        public Usuario GetCurrentUser() => _currentUser;

        // Define o usuário atual
        public void SetCurrentUser(Usuario user) => _currentUser = user;

        // Dicionário para armazenar os estados dos usuários
        private readonly ConcurrentDictionary<string, UserState> _userStates = new();

        // Obtém ou cria o estado do usuário
        public UserState GetUserState(string userId)
            => _userStates.GetOrAdd(userId, id => new UserState());

        // Atualiza o estado do usuário
        public void UpdateUserState(string userId, UserState state)
            => _userStates[userId] = state;

        // Remove o estado do usuário
        public void RemoveUserState(string userId)
            => _userStates.TryRemove(userId, out _);
    }

}
