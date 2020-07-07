using System;
using System.Collections.Generic;
using System.Text;

namespace TesteIMCApplication.Base
{
    public abstract class BaseResponse
    {
        public BaseResponse()
        {
            IsSuccess = true;
            Errors = new List<string>();
        }

        public bool IsSuccess { get; set; }
        public IList<string> Errors { get; private set; }

        public void SetFail(IEnumerable<string> errors)
        {
            this.IsSuccess = false;
            foreach (var e in errors)
            {
                this.Errors.Add(e);
            }
        }
    }
}
