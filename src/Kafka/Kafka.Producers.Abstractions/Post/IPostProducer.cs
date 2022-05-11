﻿using Kafka.Producers.Abstractions.Base;

namespace Kafka.Producers.Abstractions.Post;

public interface IPostProducer
{
    Task ProduceAsync(string key, KafkaMessage message, CancellationToken ct);
}