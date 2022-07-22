using System.Collections.Generic;

namespace WorkerService.Core.Dtos
{
    public class ResultList<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<T> Data { get; set; }

        public static ResultList<T> CreateSuccess(List<T> data) => new ResultList<T> { Success = true, Message = "Operação realizada com sucesso", Data = data };
        public static ResultList<T> CreateError(string message) => new ResultList<T> { Success = false, Message = message };
    }
}
