package com.chattalk.auth.common.filter;

import jakarta.servlet.ServletInputStream;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletRequestWrapper;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.nio.charset.StandardCharsets;

public class CachedBodyRequestWrapper extends HttpServletRequestWrapper {
    private final byte[] cachedBody;

    public CachedBodyRequestWrapper(HttpServletRequest request) throws IOException {
        super(request);

        this.cachedBody = request.getInputStream().readAllBytes();
    }

    @Override
    public ServletInputStream getInputStream() throws IOException {
        ByteArrayInputStream byteArrayInputStream = new ByteArrayInputStream(cachedBody);

        return new CachedBodyServletInputStream(byteArrayInputStream);
    }

    public String getBody() {
        return new String(cachedBody, StandardCharsets.UTF_8);
    }
}
