package com.chattalk.auth.legacy.dto.request;

import lombok.Getter;

@Getter
public class LoginRequest {
    private String userId;
    private String password;
}