using System.Collections.Generic;
using System.Linq;

namespace POS.Shared.Helpers
{
    public class ApiReturn<TData>
    {
        public int Count { get; set; }

        public ApiReturn()
        {
            Errors = new List<Error>();
        }

        public TData Data { get; set; }
        public List<Error> Errors { get; set; }

        public bool HasErrors
        {
            get { return Errors.Count > 0; }
        }

        public void AddError(string error)
        {
            Errors.Add(new Error(error));
        }

        public void AddTechnicalErrorEn()
        {
            Errors.Add(new Error("Something went wrong !"));
        }


        public void AddTechnicalInsertErrorEN()
        {
            Errors.Add(new Error("Error while inserting data"));
        }

        public void AddNotFoundErrorEn()
        {
            Errors.Add(new Error("No Data Found"));
        }
     
    }
}
