package com.chattalk.auth.dto.response;

import lombok.Getter;

@Getter
public class LoginResponse {
    private Long id;
    private String userId;
    private String userName;

    public LoginResponse(Long id, String userId, String userName) {
        this.id = id;
        this.userId = userId;
        this.userName = userName;
    }
}
