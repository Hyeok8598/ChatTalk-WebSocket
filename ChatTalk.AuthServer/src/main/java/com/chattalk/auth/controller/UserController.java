package com.chattalk.auth.controller;

import com.chattalk.auth.dto.request.ChangeRequest;
import com.chattalk.auth.dto.request.LoginRequest;
import com.chattalk.auth.dto.request.SignUpRequest;
import com.chattalk.auth.dto.response.ChangeResponse;
import com.chattalk.auth.dto.response.LoginResponse;
import com.chattalk.auth.dto.response.SignUpResponse;
import com.chattalk.auth.service.UserService;
import org.springframework.web.bind.annotation.*;

@CrossOrigin(origins = {
        "http://127.0.0.1:5173",
        "http://localhost:5173"
})
@RestController
@RequestMapping("/api/users")
public class UserController {
    private final UserService userService;

    public UserController(UserService userService) {
        this.userService = userService;
    }

    @PostMapping("/signup")
    public SignUpResponse signUp(@RequestBody SignUpRequest signUpRequest) {
        return userService.signUp(signUpRequest);
    }

    @PostMapping("/login")
    public LoginResponse login(@RequestBody LoginRequest loginRequest) {
        return userService.login(loginRequest);
    }

    @PostMapping("/change")
    public ChangeResponse change(@RequestBody ChangeRequest changeRequest) {
        return userService.change(changeRequest);
    }
}