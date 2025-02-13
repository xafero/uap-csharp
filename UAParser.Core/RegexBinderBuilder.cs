//
// Copyright Atif Aziz, Søren Enemærke
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

namespace UAParser;

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

internal static class RegexBinderBuilder
{
    public static Func<Match, IEnumerator<int>, TResult> SelectMany<T1, T2, TResult>(
        this Func<Match, IEnumerator<int>, T1> binder,
        Func<T1, Func<Match, IEnumerator<int>, T2>> continuation,
        Func<T1, T2, TResult> projection)
    {
        return (m, num) =>
            {
                var bound = binder(m, num);
                var continued = continuation(bound)(m, num);
                var projected = projection(bound, continued);
                return projected;
            };
    }
}
