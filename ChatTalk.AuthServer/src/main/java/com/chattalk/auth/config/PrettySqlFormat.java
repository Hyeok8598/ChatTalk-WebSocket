package com.chattalk.auth.config;

import com.github.vertical_blank.sqlformatter.SqlFormatter;
import com.p6spy.engine.spy.appender.MessageFormattingStrategy;

public class PrettySqlFormat implements MessageFormattingStrategy {
    @Override
    public String formatMessage(
            int connectionId,
            String now,
            long elapsed,
            String category,
            String prepared,
            String sql,
            String url) {

        if(sql == null || sql.trim().isEmpty()) {
            return "";
        }

        return "\n" + SqlFormatter.format(sql);
    }
}
