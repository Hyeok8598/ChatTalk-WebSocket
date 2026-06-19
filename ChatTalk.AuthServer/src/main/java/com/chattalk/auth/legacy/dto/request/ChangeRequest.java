package com.chattalk.auth.legacy.dto.request;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class ChangeRequest {
    private String userId;
    private String userName;
    private String beforePassword;
    private String afterPassword;
}
