// This source code is dual-licensed under the Apache License, version
// 2.0, and the Mozilla Public License, version 2.0.
//
// The APL v2.0:
//
//---------------------------------------------------------------------------
//   Copyright (c) 2007-2020 VMware, Inc.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       https://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//---------------------------------------------------------------------------
//
// The MPL v2.0:
//
//---------------------------------------------------------------------------
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
//
//  Copyright (c) 2007-2020 VMware, Inc.  All rights reserved.
//---------------------------------------------------------------------------

using System.Text;
using RabbitMQ.Client.client.framing;

namespace RabbitMQ.Client.Framing.Impl
{
    internal sealed class QueueDeclareOk : Client.Impl.MethodBase
    {
        public string _queue;
        public uint _messageCount;
        public uint _consumerCount;

        public QueueDeclareOk()
        {
        }

        public QueueDeclareOk(string Queue, uint MessageCount, uint ConsumerCount)
        {
            _queue = Queue;
            _messageCount = MessageCount;
            _consumerCount = ConsumerCount;
        }

        public override ProtocolCommandId ProtocolCommandId => ProtocolCommandId.QueueDeclareOk;
        public override string ProtocolMethodName => "queue.declare-ok";
        public override bool HasContent => false;

        public override void ReadArgumentsFrom(ref Client.Impl.MethodArgumentReader reader)
        {
            _queue = reader.ReadShortstr();
            _messageCount = reader.ReadLong();
            _consumerCount = reader.ReadLong();
        }

        public override void WriteArgumentsTo(ref Client.Impl.MethodArgumentWriter writer)
        {
            writer.WriteShortstr(_queue);
            writer.WriteLong(_messageCount);
            writer.WriteLong(_consumerCount);
        }

        public override int GetRequiredBufferSize()
        {
            int bufferSize = 1 + 4 + 4; // bytes for length of _queue, _messageCount, _consumerCount
            bufferSize += Encoding.UTF8.GetByteCount(_queue); // _queue in bytes
            return bufferSize;
        }
    }
}
