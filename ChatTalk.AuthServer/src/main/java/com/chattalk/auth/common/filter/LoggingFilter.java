package com.chattalk.auth.common.filter;

import jakarta.servlet.FilterChain;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Component;
import org.springframework.web.filter.OncePerRequestFilter;
import org.springframework.web.util.ContentCachingResponseWrapper;

import java.io.IOException;
import java.nio.charset.StandardCharsets;

@Slf4j
@Component
public class LoggingFilter extends OncePerRequestFilter {
    @Override
    protected void doFilterInternal(HttpServletRequest request, HttpServletResponse response, FilterChain filterChain) throws ServletException, IOException {
        String contentType = request.getContentType();

        boolean isMultipart =
                contentType != null &&
                contentType.toLowerCase().startsWith("multipart/");

        if (isMultipart) {
            log.info("[Request] {} {} multipart",
                    request.getMethod(),
                    request.getRequestURL());

            filterChain.doFilter(request, response);
            return;
        }

        CachedBodyRequestWrapper requestWrapper = new CachedBodyRequestWrapper(request);
        String requestBody = requestWrapper.getBody();
        ContentCachingResponseWrapper responseWrapper = new ContentCachingResponseWrapper(response);


        log.info("""
                    \n
                    [Request]
                    {} {}
                    BODY={}
                """,
                request.getMethod(),
                request.getRequestURL(),
                requestBody
        );

        long startTime = System.currentTimeMillis();

        try {
            filterChain.doFilter(requestWrapper, responseWrapper);
        } finally {
            long elapsedTime = System.currentTimeMillis() - startTime;
            String responseBody = new String(responseWrapper.getContentAsByteArray(), StandardCharsets.UTF_8);
            log.info("""
                        \n
                        [Response]
                        STAUS={} | time={}ms
                        BODY={}
                    """,
                    responseWrapper.getStatus(),
                    elapsedTime,
                    responseBody
            );

            responseWrapper.copyBodyToResponse();
        }
    }
}
