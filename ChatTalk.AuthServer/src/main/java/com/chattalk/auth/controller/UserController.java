package com.chattalk.auth.controller;

import com.chattalk.auth.dto.request.LoginRequest;
import com.chattalk.auth.dto.request.SignUpRequest;
import com.chattalk.auth.dto.response.LoginResponse;
import com.chattalk.auth.service.UserService;
import org.springframework.web.bind.annotation.*;

@CrossOrigin(origins = "http://127.0.0.1:5500")
@RestController
@RequestMapping("/api/users")
public class UserController {
    private final UserService userService;

    public UserController(UserService userService) {
        this.userService = userService;
    }

    @PostMapping("/signup")
    public void signUp(@RequestBody SignUpRequest signUpRequest) {
        userService.signUp(signUpRequest);
    }

    @PostMapping("/login")
    public LoginResponse login(@RequestBody LoginRequest loginRequest) {
        return userService.login(loginRequest);
    }
}