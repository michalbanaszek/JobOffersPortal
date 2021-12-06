﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Common.Models
{
    public class Result
    {
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }

        internal Result()
        {
            Succeeded = true;
            Errors = Array.Empty<string>();
        }

        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public static Result Success()
        {
            return new Result(true, Array.Empty<string>());
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    }
}