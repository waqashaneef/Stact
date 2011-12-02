// Copyright 2010 Chris Patterson
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Stact.Configuration.Internal
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using Behaviors;


    public class ExceptionHandlerMethodApplicator<TState, TBehavior> :
        ActorBehaviorApplicator<TState, TBehavior>
        where TBehavior : Behavior<TState>
    {
        Action<TBehavior, Exception, NextExceptionHandler> _handler;

        public ExceptionHandlerMethodApplicator(MethodInfo method)
        {
            _handler = GenerateHandler(method);
        }

        public void Apply(BehaviorContext<TState, TBehavior> context)
        {
            // context.Receive<Message<TMessage>>(message => _consumer(context.Behavior, message.Body));
            // TODO implement application to context
        }

        static Action<TBehavior, Exception, NextExceptionHandler> GenerateHandler(MethodInfo method)
        {
            ParameterExpression behavior = Expression.Parameter(typeof(TBehavior), "behavior");
            ParameterExpression exception = Expression.Parameter(typeof(Exception), "exception");
            ParameterExpression next = Expression.Parameter(typeof(NextExceptionHandler), "next");
            MethodCallExpression call = Expression.Call(behavior, method, exception, next);

            var parameters = new[] {behavior, exception, next};
            return Expression.Lambda<Action<TBehavior, Exception, NextExceptionHandler>>(call, parameters)
                .Compile();
        }
    }
}