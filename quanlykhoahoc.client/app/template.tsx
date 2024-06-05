"use client";

import React from "react";
import { ToastContainer } from "react-toastify";
import { MantineProvider } from "@mantine/core";
import { ModalsProvider } from "@mantine/modals";
import { Provider } from "react-redux";
import store from "../lib/store";

export default function Template({ children }: { children: React.ReactNode }) {
  return (
    <Provider store={store}>
      <ToastContainer />
      <MantineProvider>
        <ModalsProvider />
        {children}
      </MantineProvider>
    </Provider>
  );
}
