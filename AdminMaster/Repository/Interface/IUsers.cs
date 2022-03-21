using AdminMaster.Utils.Enums;
using AdminMaster.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminMaster.Repository.Interface
{
    public interface IUsers
    {
        SignInEnum SingIn(SignInModel model);
        SignUpEnum SingUp(SignUpModel model);
        public bool VerifyAccount(string Otp);
    }
}
