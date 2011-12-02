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
namespace Stact.Behaviors
{
    using Configuration.Internal;


    public class BehaviorHandleImpl<TState, TBehavior> :
        BehaviorHandle,
        BehaviorContext<TState, TBehavior>
        where TBehavior : Behavior<TState>
    {
        readonly Actor<TState> _actor;
        readonly TBehavior _behavior;

        public BehaviorHandleImpl(Actor<TState> actor, TBehavior behavior)
        {
            _actor = actor;
            _behavior = behavior;
        }

        public void Receive<TMessage>(Consumer<Message<TMessage>> consumer)
        {
            PendingReceive pendingReceive = _actor.Receive(consumer);
        }

        public TBehavior Behavior
        {
            get { return _behavior; }
        }

        public void Remove()
        {
        }
    }
}