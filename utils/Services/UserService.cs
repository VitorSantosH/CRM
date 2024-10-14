namespace CRM.Services
{
    public class UserService
    {
        private readonly UserStateManager _userStateManager;

        public UserService(UserStateManager userStateManager)
        {
            _userStateManager = userStateManager;
        }

        public void LoginUser(string userId)
        {
            var userState = _userStateManager.GetUserState(userId);
            // Lógica de login e inicialização do estado do usuário
        }

        public void UpdateUserInfo(string userId, string nome, string telefone)
        {
            var userState = _userStateManager.GetUserState(userId);
            userState.Nome = nome;
            userState.Telefone = telefone;

            // Atualize o estado no gerenciador
            _userStateManager.UpdateUserState(userId, userState);
        }
    }
}
