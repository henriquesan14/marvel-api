﻿namespace Marvel.API.ViewModels
{
    public class DataResult<T>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public IList<T> Results { get; set; }
    }
}
