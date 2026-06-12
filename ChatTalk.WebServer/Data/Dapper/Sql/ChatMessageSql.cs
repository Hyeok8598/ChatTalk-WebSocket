namespace ChatTalk.WebServer.Data.Dapper.Sql
{
    public static class ChatMessageSql
    {
        public static readonly string INSERT_001 = """
            INSERT INTO CHAT_MESSAGE (
                MESSAGE_ID,
                SENDER_NAME,
                CONTENT
            )
            VALUES (
                @MessageId,
                @SenderName,
                @Content
            );
            """;
    }
}
