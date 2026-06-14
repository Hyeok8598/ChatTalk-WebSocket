package com.chattalk.auth.dto.response;

import lombok.Getter;

@Getter
public class LoginResponse {
    private String userId;
    private String userName;

    public LoginResponse(String userId, String userName) {
        this.userId = userId;
        this.userName = userName;
    }
}
