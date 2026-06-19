package com.chattalk.auth.controller;

import com.chattalk.auth.common.DataMap;
import com.chattalk.auth.service.UserService;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/users")
public class UserController {
    private final UserService userService;

    public UserController(UserService userService) {
        this.userService = userService;
    }

    @PostMapping("/signup")
    public int signUp(@RequestBody DataMap dataMap) {
        return userService.signUp(dataMap);
    }

    @PostMapping("/login")
    public DataMap login(@RequestBody DataMap dataMap) {
        return userService.login(dataMap);
    }

    @PostMapping("/change")
    public int change(@RequestBody DataMap dataMap) {
        return userService.change(dataMap);
    }

    @PostMapping("/search")
    public DataMap search(@RequestBody DataMap dataMap) {
        return userService.search(dataMap);
    }
}