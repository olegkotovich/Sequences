﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sequences.Tests.Functional.Extensions;
using Xunit;

namespace Sequences.Tests.Functional
{
    public class Fibonacci
    {
        private readonly int[] _expectedFibs = { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 };

        [Fact]
        public void Declarative()
        {
            ISequence<int> fibs = null;
            fibs = Sequence.With(0, 1)              //start with 0, 1...
                .Concat(() =>                       //and then
                    fibs.Zip(fibs.Tail)             //zip the sequence with its tail (i.e., (0,1), (1,1), (1,2), (2,3), (3, 5))
                        .Select(TupleEx.Sum));      //select the sum of each pair (i.e., 1, 2, 3, 5, 8)

            Assert.Equal(_expectedFibs, fibs.Take(10));
        }

        [Fact]
        public void Loop()
        {
            var fibs = Fibs(0, 1);

            Assert.Equal(_expectedFibs, fibs.Take(10));
        }

        //current and next are any two consecutive fibonacci numbers.
        //e.g., 0 and 1, or 5 and 8
        private ISequence<int> Fibs(int current, int next)
        {
            return new Sequence<int>(current, () => Fibs(next, current + next));
        }
    }
}
