-- DDL
-- CHAT_MESSAGE
CREATE TABLE chat_message (
	id BIGSERIAL PRIMARY KEY,
	message_id VARCHAR(36) NOT NULL,
	message_type VARCHAR(20) NOT NULL,
	sender_user_id VARCHAR(50) NOT NULL,
	target_user_id VARCHAR(50),
	content TEXT NOT NULL,
	create_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- USERS
CREATE TABLE public.users
(
    id          BIGSERIAL PRIMARY KEY,
    create_at   TIMESTAMP NOT NULL DEFAULT now(),
    password    VARCHAR(255) NOT NULL,
    user_id     VARCHAR(255) NOT NULL UNIQUE,
    user_name   VARCHAR(255) NOT NULL
);

commit;